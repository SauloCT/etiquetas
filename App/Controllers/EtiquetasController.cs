using App.Models;
using App.Services.Interfaces;
using App.VendaERP.Core.Models;
using static App.Services.Interfaces.IEtiquetasService;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using VendaERP.Core;
using VendaERP.Core.Models;

namespace App.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de etiquetas
    /// Refatorado para seguir princípios SOLID e boas práticas de arquitetura
    /// </summary>
    public class EtiquetasController : Controller
    {
        private readonly IEtiquetasService _etiquetasService;
        private readonly IValidationService _validationService;
        private readonly ILogger<EtiquetasController> _logger;
        private readonly IAutocompletarService _autocompletarService;

        public EtiquetasController(
            IEtiquetasService etiquetasService,
            IValidationService validationService,
            ILogger<EtiquetasController> logger,
            IAutocompletarService autocompletarService)
        {
            _etiquetasService = etiquetasService ?? throw new ArgumentNullException(nameof(etiquetasService));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _autocompletarService = autocompletarService ?? throw new ArgumentNullException(nameof(autocompletarService));
        }

        /// <summary>
        /// Página principal de etiquetas
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Acessando página principal de etiquetas");

                if (TempData.ContainsKey("message"))
                    ViewBag.message = TempData["message"];

                var autocompletarData = await _autocompletarService.GetAutocompletarDataAsync();
                
                _logger.LogDebug("Dados de autocompletar carregados com sucesso");
                return View(autocompletarData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar página principal de etiquetas");
                TempData["message"] = "Erro ao carregar a página. Tente novamente.";
                return View(new Autocompletar());
            }
        }

        /// <summary>
        /// Página para criar novo modelo
        /// </summary>
        public IActionResult NewModel()
        {
            try
            {
                _logger.LogDebug("Acessando página de criação de modelo");
            return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar página de criação de modelo");
                TempData["message"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Salva um novo modelo de etiqueta
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SaveNewModel(
            string nome, string papel, string larguraPapel, string alturaPapel, 
            string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal, 
            string? espacamentoVertical, string? margemEsquerda, string? margemSuperior, 
            string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de novo modelo: {Nome}", nome);

                // Validações básicas antes de criar o modelo
                if (string.IsNullOrWhiteSpace(nome))
                {
                    _logger.LogWarning("Tentativa de criar modelo sem nome");
                    TempData["message"] = "Nome do modelo é obrigatório.";
                    return RedirectToAction("NewModel");
                }

                // Criar modelo com validação
                var modelo = CriarModeloFromParameters(
                    nome, papel, larguraPapel, alturaPapel, larguraEtiqueta, alturaEtiqueta,
                    espacamentoHorizontal, espacamentoVertical, margemEsquerda, margemSuperior,
                    zoomImpressao, tamanhoFonte, tamanhoPreco, alturaBarras);

                var modeloCriado = await _etiquetasService.CriarModeloAsync(modelo);

                _logger.LogInformation("Modelo criado com sucesso. ID: {Id}, Nome: {Nome}", 
                    modeloCriado.Id, modeloCriado.Nome);
                
                TempData["message"] = "Modelo criado com sucesso!";
                return RedirectToAction("ListModels");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dados inválidos ao criar modelo: {Nome}", nome);
                TempData["message"] = $"Erro de validação: {ex.Message}";
                return RedirectToAction("NewModel");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Operação inválida ao criar modelo: {Nome}", nome);
                TempData["message"] = ex.Message;
                return RedirectToAction("NewModel");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar modelo: {Nome}", nome);
                TempData["message"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("NewModel");
            }
        }

        /// <summary>
        /// Lista modelos com paginação
        /// </summary>
        public async Task<IActionResult> ListModels(int pageNumber = 1)
        {
            try
            {
                _logger.LogDebug("Listando modelos. Página: {PageNumber}", pageNumber);

                const int pageSize = 15;
                var (modelos, totalCount) = await _etiquetasService.ListarModelosAsync(pageNumber, pageSize);

                var pager = new Pager(totalCount, pageNumber, pageSize);
                ViewBag.pager = pager;

                return View(modelos);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Parâmetros inválidos ao listar modelos");
                TempData["message"] = $"Erro de validação: {ex.Message}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar modelos");
                TempData["message"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Remove um modelo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RemoveModel(string id)
        {
            try
            {
                _logger.LogInformation("Iniciando remoção de modelo. ID: {Id}", id);

                // Validação básica do ID
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogWarning("ID vazio fornecido para remoção de modelo");
                    return BadRequest(new { success = false, message = "ID do modelo é obrigatório" });
                }

                // Verificar se o modelo existe antes de tentar remover
                var modeloExistente = await _etiquetasService.ObterModeloPorIdAsync(id);
                if (modeloExistente == null)
                {
                    _logger.LogWarning("Tentativa de remover modelo inexistente. ID: {Id}", id);
                    return NotFound(new { success = false, message = "Modelo não encontrado" });
                }

                var sucesso = await _etiquetasService.RemoverModeloAsync(id);

                if (sucesso)
                {
                    _logger.LogInformation("Modelo removido com sucesso. ID: {Id}, Nome: {Nome}", id, modeloExistente.Nome);
                    return Ok(new { success = true, message = $"Modelo '{modeloExistente.Nome}' removido com sucesso" });
                }
                else
                {
                    _logger.LogWarning("Falha ao remover modelo. ID: {Id}", id);
                    return StatusCode(500, new { success = false, message = "Falha ao remover o modelo" });
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "ID inválido ao remover modelo: {Id}", id);
                return BadRequest(new { success = false, message = $"ID inválido: {ex.Message}" });
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Erro de banco de dados ao remover modelo. ID: {Id}", id);
                return StatusCode(500, new { success = false, message = "Erro de conexão com o banco de dados" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover modelo. ID: {Id}", id);
                return StatusCode(500, new { success = false, message = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Página de edição de modelo
        /// </summary>
        [Route("Etiquetas/EditModel/{id}")]
        public async Task<IActionResult> EditModel(string id)
        {
            try
            {
                _logger.LogDebug("Acessando página de edição. ID: {Id}", id);

                var modelo = await _etiquetasService.ObterModeloPorIdAsync(id);
                if (modelo == null)
                {
                    TempData["message"] = "Modelo não encontrado.";
                    return RedirectToAction("ListModels");
                }

                ViewBag.modelo = modelo;
            return View();
        }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "ID inválido ao editar modelo: {Id}", id);
                TempData["message"] = $"Erro de validação: {ex.Message}";
                return RedirectToAction("ListModels");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar página de edição. ID: {Id}", id);
                TempData["message"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("ListModels");
            }
        }

        /// <summary>
        /// Atualiza um modelo existente
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateModel(
            string id, string nome, string papel, string larguraPapel, string alturaPapel,
            string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal,
            string? espacamentoVertical, string? margemEsquerda, string? margemSuperior,
            string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            try
            {
                _logger.LogInformation("Iniciando atualização de modelo. ID: {Id}", id);

                // Criar modelo com validação
                var modelo = CriarModeloFromParameters(
                    nome, papel, larguraPapel, alturaPapel, larguraEtiqueta, alturaEtiqueta,
                    espacamentoHorizontal, espacamentoVertical, margemEsquerda, margemSuperior,
                    zoomImpressao, tamanhoFonte, tamanhoPreco, alturaBarras);

                var modeloAtualizado = await _etiquetasService.AtualizarModeloAsync(id, modelo);

                if (modeloAtualizado == null)
                {
                    TempData["message"] = "Modelo não encontrado.";
                    return RedirectToAction("ListModels");
                }

                TempData["message"] = "Modelo atualizado com sucesso!";
                _logger.LogInformation("Modelo atualizado com sucesso. ID: {Id}", id);

                return RedirectToAction("EditModel", new { id });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dados inválidos ao atualizar modelo. ID: {Id}", id);
                TempData["message"] = $"Erro de validação: {ex.Message}";
                return RedirectToAction("EditModel", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar modelo. ID: {Id}", id);
                TempData["message"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("EditModel", new { id });
            }
        }

        /// <summary>
        /// Processa a geração de etiquetas
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Baixar(
            string empresa, string etiqueta, string clienteFornecedor, string tabelaDePreco, 
            string deposito, bool dadoLadoCodigoBarras, bool imprimirCodigoBarras, 
            bool imprimirNumeroCodigoBarras, bool imprimirCodigo, bool imprimirNome, 
            bool imprimirPreco, bool precoComoCodigo, bool imprimirMarca, bool imprimirBorda, 
            bool imprimirLote, bool imprimirNumeroSerie, bool gerarCodigosBarras, string[] itens)
        {
            try
            {
                _logger.LogInformation("Iniciando processamento de etiquetas para empresa: {Empresa}", empresa);

                var request = new EtiquetasRequest
                {
                    Empresa = empresa,
                    Etiqueta = etiqueta,
                    ClienteFornecedor = clienteFornecedor,
                    TabelaDePreco = tabelaDePreco,
                    Deposito = deposito,
                    DadoLadoCodigoBarras = dadoLadoCodigoBarras,
                    ImprimirCodigoBarras = imprimirCodigoBarras,
                    ImprimirNumeroCodigoBarras = imprimirNumeroCodigoBarras,
                    ImprimirCodigo = imprimirCodigo,
                    ImprimirNome = imprimirNome,
                    ImprimirPreco = imprimirPreco,
                    PrecoComoCodigo = precoComoCodigo,
                    ImprimirMarca = imprimirMarca,
                    ImprimirBorda = imprimirBorda,
                    ImprimirLote = imprimirLote,
                    ImprimirNumeroSerie = imprimirNumeroSerie,
                    GerarCodigosBarras = gerarCodigosBarras,
                    Itens = itens ?? Array.Empty<string>()
                };

                var result = await _etiquetasService.ProcessarEtiquetasAsync(request);

                // Se for uma requisição AJAX, retorna apenas a view parcial
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_EtiquetasContent", new EtiquetasContentViewModel
                    {
                        NomeEmpresa = result.NomeEmpresa,
                        ModelEtiqueta = result.ModelEtiqueta,
                        OpcoesSelecionadas = result.OpcoesSelecionadas,
                        ListaItens = result.ListaItens
                    });
                }

                // Se não for AJAX, retorna a view completa
                ViewBag.nomeEmpresa = result.NomeEmpresa;
                ViewBag.modelEtiqueta = result.ModelEtiqueta;
                ViewBag.opcoesSelecionadas = result.OpcoesSelecionadas;
                ViewBag.listaItens = result.ListaItens;

                _logger.LogInformation("Processamento de etiquetas concluído com sucesso. {Count} itens", result.ListaItens.Count);
                return View();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dados inválidos ao processar etiquetas");
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(new { success = false, message = ex.Message });
                }
                
                TempData["message"] = $"Erro de validação: {ex.Message}";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Operação inválida ao processar etiquetas");
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(new { success = false, message = ex.Message });
                }
                
                TempData["message"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar etiquetas");
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return StatusCode(500, new { success = false, message = "Erro interno do servidor" });
                }
                
                TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Verifica produtos sem código de barras
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> VerificarProdutosSemCodigo(string[] itens)
        {
            try
            {
                _logger.LogDebug("Verificando produtos sem código. {Count} itens", itens?.Length ?? 0);

                var produtosSemCodigo = await _etiquetasService.VerificarProdutosSemCodigoAsync(itens);

                return Json(new
                {
                    success = true,
                    produtos = produtosSemCodigo.Select(p => new { p.Id, p.Nome }),
                    total = produtosSemCodigo.Count
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dados inválidos ao verificar produtos sem código");
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar produtos sem código");
                return Json(new { success = false, message = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Obtém dados de um produto
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetProduto(string deposito, string produto)
        {
            try
            {
                _logger.LogDebug("Buscando produto. ID: {Produto}, Depósito: {Deposito}", produto, deposito);

                if (string.IsNullOrWhiteSpace(produto))
                {
                    _logger.LogWarning("ID do produto está vazio ou nulo");
                    return Json(new { error = "ID do produto é obrigatório" });
                }

                var produtoEscolhido = await _etiquetasService.ObterProdutoAsync(deposito, produto);

                if (produtoEscolhido == null)
                {
                    _logger.LogWarning("Produto não encontrado. ID: {Produto}", produto);
                    return Json(new { error = "Produto não encontrado" });
                }

                _logger.LogDebug("Produto encontrado com sucesso. ID: {Produto}, Nome: {Nome}", 
                    produto, produtoEscolhido.Nome);

                // Retornar objeto JSON estruturado
                var resultado = new
                {
                    Id = produtoEscolhido.Id,
                    Codigo = produtoEscolhido.Codigo ?? "",
                    CodigoNFe = produtoEscolhido.Codigo ?? "",
                    CodigoBarras = produtoEscolhido.CodigoBarras ?? "",
                    Nome = produtoEscolhido.Nome ?? "",
                    PrecoVenda = produtoEscolhido.Preco.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                    Marca = produtoEscolhido.Marca ?? "",
                    NumeroSerie = produtoEscolhido.NumeroSerie ?? "",
                    Unidade = "UN" // Valor padrão para compatibilidade
                };

                return Json(resultado);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dados inválidos ao buscar produto: {Produto}", produto);
                return Json(new { error = $"Dados inválidos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto. ID: {Produto}", produto);
                return Json(new { error = "Erro interno do servidor" });
            }
        }

        /// <summary>
        /// Página de erro
        /// </summary>
        public IActionResult Error()
        {
            _logger.LogDebug("Acessando página de erro");
            return View("Error");
        }

        #region Métodos Privados

        /// <summary>
        /// Cria um modelo de etiqueta a partir dos parâmetros com validação
        /// </summary>
        private DtoEtiquetasPadroes CriarModeloFromParameters(
            string nome, string papel, string larguraPapel, string alturaPapel,
            string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal,
            string? espacamentoVertical, string? margemEsquerda, string? margemSuperior,
            string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            try
            {
                // Sanitizar e validar entradas
                nome = _validationService.SanitizeString(nome);

                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new ArgumentException("Nome do modelo é obrigatório");
                }

                if (!Enum.TryParse<TipoPapel>(papel, out var tipoPapel))
                {
                    throw new ArgumentException($"Tipo de papel inválido: {papel}");
                }

                // Validar dimensões obrigatórias
                var larguraPapelValue = _validationService.ValidarEConverterDecimal(larguraPapel, "Largura do papel");
                var alturaPapelValue = _validationService.ValidarEConverterDecimal(alturaPapel, "Altura do papel");

                if (!larguraPapelValue.HasValue || larguraPapelValue <= 0)
                {
                    throw new ArgumentException("Largura do papel deve ser um valor positivo");
                }

                if (!alturaPapelValue.HasValue || alturaPapelValue <= 0)
                {
                    throw new ArgumentException("Altura do papel deve ser um valor positivo");
                }

                var modelo = new DtoEtiquetasPadroes
                {
                    Nome = nome,
                    Papel = tipoPapel,
                    LarguraPapel = (double)larguraPapelValue.Value,
                    AlturaPapel = (double)alturaPapelValue.Value,
                    Largura = (double?)_validationService.ValidarEConverterDecimal(larguraEtiqueta, "Largura da etiqueta"),
                    Altura = (double?)_validationService.ValidarEConverterDecimal(alturaEtiqueta, "Altura da etiqueta"),
                    EspacamentoHorizontal = (double?)_validationService.ValidarEConverterDecimal(espacamentoHorizontal, "Espaçamento horizontal"),
                    EspacamentoVertical = (double?)_validationService.ValidarEConverterDecimal(espacamentoVertical, "Espaçamento vertical"),
                    MargemEsquerda = (double?)_validationService.ValidarEConverterDecimal(margemEsquerda, "Margem esquerda"),
                    MargemSuperior = (double?)_validationService.ValidarEConverterDecimal(margemSuperior, "Margem superior"),
                    ZoomImpressao = (double?)_validationService.ValidarEConverterDecimal(zoomImpressao, "Zoom de impressão"),
                    TamanhoFonte = tamanhoFonte,
                    TamanhoPreco = tamanhoPreco,
                    AlturaEAN = (double?)_validationService.ValidarEConverterDecimal(alturaBarras, "Altura das barras")
                };

                return modelo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar modelo a partir dos parâmetros");
                throw;
            }
        }

        #endregion
    }
}
