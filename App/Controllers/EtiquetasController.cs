using App.Models;
using App.VendaERP.Core.Models;
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
    public class EtiquetasController : Controller
    {
        private readonly DBAccess _db;
        private Autocompletar autocompletar;
        public EtiquetasController(DBAccess db)
        {
            _db = db;
            this.autocompletar = new Autocompletar(db);
        }
        public IActionResult Index()
        {
            if (TempData.ContainsKey("message"))
                ViewBag.message = TempData["message"];
            return View(this.autocompletar);
        }
        public IActionResult NewModel()
        {
            return View();
        }
        public IActionResult SaveNewModel(string nome, string papel, string larguraPapel, string alturaPapel, string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal, string? espacamentoVertical, string? margemEsquerda, string? margemSuperior, string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            DtoEtiquetasPadroes padrao = new DtoEtiquetasPadroes()
            {
                Nome = nome,
                Papel = (TipoPapel)Enum.Parse(typeof(TipoPapel), papel),
                LarguraPapel = Double.Parse(larguraPapel.Replace(".", ",")),
                AlturaPapel = Double.Parse(alturaPapel.Replace(".", ",")),
                Largura = larguraEtiqueta != null ? Double.Parse(larguraEtiqueta.Replace(".", ",")) : null,
                Altura = alturaEtiqueta != null ? Double.Parse(alturaEtiqueta.Replace(".", ",")) : null,
                EspacamentoHorizontal = espacamentoHorizontal != null ? Double.Parse(espacamentoHorizontal.Replace(".", ",")) : null,
                EspacamentoVertical = espacamentoVertical != null ? Double.Parse(espacamentoVertical.Replace(".", ",")) : null,
                MargemEsquerda = margemEsquerda != null ? Double.Parse(margemEsquerda.Replace(".", ",")) : null,
                MargemSuperior = margemSuperior != null ? Double.Parse(margemSuperior.Replace(".", ",")) : null,
                ZoomImpressao = zoomImpressao != null ? Double.Parse(zoomImpressao.Replace(".", ",")) : null,
                TamanhoFonte = tamanhoFonte,
                TamanhoPreco = tamanhoPreco,
                AlturaEAN = alturaBarras != null ? Double.Parse(alturaBarras.Replace(".", ",")) : null
            };
            _db._repositoryEtiquetasPadroes.Collection.InsertOne(padrao);
            return Redirect("/Etiquetas/ListModels");
        }
        public IActionResult ListModels(int pageNumber)
        {
            var listModels = _db._repositoryEtiquetasPadroes.Collection.Find(x => true).Sort("{_id: -1}").ToList();
            var pager = new Pager(listModels.Count(), pageNumber, 15);
            var model = listModels.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View(model);
        }
        public HttpResponseMessage RemoveModel(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _db._repositoryEtiquetasPadroes.Collection.DeleteOne(x => x.Id == id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [Route("Etiquetas/EditModel/{id}")]
        public IActionResult EditModel(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                this.ViewBag.modelo = _db._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == id).FirstOrDefault();
            }
            return View();
        }
        public IActionResult UpdateModel(string id, string nome, string papel, string larguraPapel, string alturaPapel, string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal, string? espacamentoVertical, string? margemEsquerda, string? margemSuperior, string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            if (!string.IsNullOrEmpty(id))
            {
                DtoEtiquetasPadroes modelo = new DtoEtiquetasPadroes()
                {
                    Id = id,
                    Nome = nome,
                    Papel = (TipoPapel)Enum.Parse(typeof(TipoPapel), papel),
                    LarguraPapel = Double.Parse(larguraPapel.Replace(".", ",")),
                    AlturaPapel = Double.Parse(alturaPapel.Replace(".", ",")),
                    Largura = larguraEtiqueta != null ? Double.Parse(larguraEtiqueta.Replace(".", ",")) : null,
                    Altura = alturaEtiqueta != null ? Double.Parse(alturaEtiqueta.Replace(".", ",")) : null,
                    EspacamentoHorizontal = espacamentoHorizontal != null ? Double.Parse(espacamentoHorizontal.Replace(".", ",")) : null,
                    EspacamentoVertical = espacamentoVertical != null ? Double.Parse(espacamentoVertical.Replace(".", ",")) : null,
                    MargemEsquerda = margemEsquerda != null ? Double.Parse(margemEsquerda.Replace(".", ",")) : null,
                    MargemSuperior = margemSuperior != null ? Double.Parse(margemSuperior.Replace(".", ",")) : null,
                    ZoomImpressao = zoomImpressao != null ? Double.Parse(zoomImpressao.Replace(".", ",")) : null,
                    TamanhoFonte = tamanhoFonte,
                    TamanhoPreco = tamanhoPreco,
                    AlturaEAN = alturaBarras != null ? Double.Parse(alturaBarras.Replace(".", ",")) : null
                };
                _db._repositoryEtiquetasPadroes.Collection.ReplaceOne(x => x.Id == id, modelo);
            }
            return Redirect("/Etiquetas/EditModel/" + id);
        }
        [HttpPost]
        public IActionResult Baixar(string empresa, string etiqueta, string clienteFornecedor, string tabelaDePreco, string deposito, bool dadoLadoCodigoBarras, bool imprimirCodigoBarras, bool imprimirNumeroCodigoBarras, bool imprimirCodigo, bool imprimirNome, bool imprimirPreco, bool precoComoCodigo, bool imprimirMarca, bool imprimirBorda, bool imprimirLote, bool imprimirNumeroSerie, bool gerarCodigosBarras, string[] itens)
        {
            try
            {
                if (itens == null || itens.Length == 0)
                {
                    TempData["message"] = "Ao menos um item deve ser inserido";
                    return Redirect("/Etiquetas/Index");
                }
                string? nomeEmpresa = null;
                try
                {
                    if(string.IsNullOrEmpty(empresa)){
                        TempData["message"] = "Erro: ID da empresa vazia ou nula.";
                        return Redirect("/Etiquetas/Index");
                    }
                    nomeEmpresa = _db._repositoryEmpresa.Collection.Find(x => x.Id == empresa)?.FirstOrDefault()?.NomeFantasia;
                    if (string.IsNullOrEmpty(nomeEmpresa))
                    {
                        TempData["message"] = "Erro: Empresa não encontrada";
                        return Redirect("/Etiquetas/Index");
                    }
                }
                catch
                {
                    TempData["message"] = "Erro: Falha ao realizar a busca no banco de dados pelo nome da empresa";
                    return Redirect("/Etiquetas/Index");
                }

                DtoEtiquetasPadroes? modelEtiqueta = null;
                try
                {
                    if (string.IsNullOrEmpty(etiqueta))
                    {
                        TempData["message"] = "Erro: ID do modelo de etiqueta vazia ou nula.";
                        return Redirect("/Etiquetas/Index");
                    }
                    modelEtiqueta = _db._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == etiqueta).FirstOrDefault();
                    if (modelEtiqueta == null)
                    {
                        TempData["message"] = "Erro: Empresa não encontrada";
                        return Redirect("/Etiquetas/Index");
                    }
                }
                catch
                {
                    TempData["message"] = "Erro: Falha ao realizar a busca no banco de dados pelo modelo de etiqueta";
                    return Redirect("/Etiquetas/Index");
                }
                
                List<ProdutoEscolhido> listaItens = new List<ProdutoEscolhido>();
                foreach(var item in itens){
                    //prod[0] == id / prod[1] == quantidade / prod[2] == lote / prod[3] == numeroSerie
                    string[] prod = item.Split(',');
                    
                    // Incluindo o campo Tamanho na projeção para a geração de códigos
                    var produtoDocument = _db._repositoryProduto.Collection.Find(x => x.Id == prod[0]).Project(new BsonDocument { 
                        { "_id", true }, 
                        { "CodigoNFe", true }, 
                        { "Nome", true }, 
                        { "PrecoVenda", true }, 
                        { "Marca", true }, 
                        { "EAN_NFe", true },
                        { "Tamanho", true }  // Adicionado para geração de códigos
                    }).FirstOrDefault();
                    
                    if (produtoDocument != null)
                    {
                        var produdo = BsonSerializer.Deserialize<ProdutoEscolhido>(produtoDocument.ToJson());
                        produdo.Quantidade = int.Parse(prod[1]);
                        if (!string.IsNullOrEmpty(prod[2]))
                            produdo.Lote = prod[2];
                        if (!string.IsNullOrEmpty(prod[3]))
                            produdo.NumeroSerie = prod[3];

                        // Se a opção de gerar códigos de barras estiver marcada e o produto não tiver código
                        if (gerarCodigosBarras && string.IsNullOrEmpty(produdo.CodigoBarras))
                        {
                            var novoCodigoBarras = GerarCodigoBarras(empresa, produdo.Id);
                            if (!string.IsNullOrEmpty(novoCodigoBarras))
                            {
                                produdo.CodigoBarras = novoCodigoBarras;
                                
                                // Atualizar o produto no banco de dados com o novo código
                                var filter = Builders<DtoProduto>.Filter.Eq("_id", ObjectId.Parse(produdo.Id));
                                var update = Builders<DtoProduto>.Update.Set("EAN_NFe", novoCodigoBarras);
                                _db._repositoryProduto.Collection.UpdateOne(filter, update);
                            }
                        }

                        listaItens.Add(produdo);
                    }
                }

                OpcoesSelecionadas opcoesSelecionadas = new OpcoesSelecionadas()
                {
                    DadoLadoCodigoBarras = dadoLadoCodigoBarras,
                    ImprimirBorda = imprimirBorda,
                    ImprimirCodigo = imprimirCodigo,
                    ImprimirCodigoBarras = imprimirCodigoBarras,
                    ImprimirLote = imprimirLote,
                    ImprimirMarca = imprimirMarca,
                    ImprimirNome = imprimirNome,
                    ImprimirNumeroCodigoBarras = imprimirNumeroCodigoBarras,
                    ImprimirNumeroSerie = imprimirNumeroSerie,
                    ImprimirPreco = imprimirPreco,
                    PrecoComoCodigo = precoComoCodigo,
                    GerarCodigosBarras = gerarCodigosBarras
                };

                // Se for uma requisição AJAX, retorna apenas a view parcial
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_EtiquetasContent", new EtiquetasContentViewModel { 
                        NomeEmpresa = nomeEmpresa,
                        ModelEtiqueta = modelEtiqueta,
                        OpcoesSelecionadas = opcoesSelecionadas,
                        ListaItens = listaItens
                    });
                }

                // Se não for AJAX, retorna a view completa como antes
                ViewBag.nomeEmpresa = nomeEmpresa;
                ViewBag.modelEtiqueta = modelEtiqueta;
                ViewBag.opcoesSelecionadas = opcoesSelecionadas;
                ViewBag.listaItens = listaItens;
                return View();
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(ex.Message);
                }
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult VerificarProdutosSemCodigo(string[] itens)
        {
            try
            {
                var produtosSemCodigo = new List<object>();
                
                foreach(var item in itens)
                {
                    string[] prod = item.Split(',');
                    var produto = _db._repositoryProduto.Collection.Find(x => x.Id == prod[0])
                        .Project(new BsonDocument { 
                            { "_id", true }, 
                            { "Nome", true }, 
                            { "EAN_NFe", true } 
                        }).FirstOrDefault();
                    
                    if (produto != null)
                    {
                        // Usar uma abordagem mais direta para acessar os valores
                        string id = produto["_id"].ToString();
                        string nome = "Nome não encontrado";
                        string eanNfe = "";
                        
                        // Verificar se o campo Nome existe
                        if (produto.Contains("Nome"))
                        {
                            var nomeValue = produto["Nome"];
                            if (nomeValue != null && !nomeValue.IsBsonNull)
                            {
                                nome = nomeValue.ToString();
                            }
                        }
                        
                        // Verificar se o campo EAN_NFe existe
                        if (produto.Contains("EAN_NFe"))
                        {
                            var eanValue = produto["EAN_NFe"];
                            if (eanValue != null && !eanValue.IsBsonNull)
                            {
                                eanNfe = eanValue.ToString();
                            }
                        }
                        
                        if (string.IsNullOrEmpty(eanNfe))
                        {
                            produtosSemCodigo.Add(new { 
                                Id = id,
                                Nome = nome
                            });
                        }
                    }
                }
                
                return Json(new { 
                    success = true, 
                    produtos = produtosSemCodigo,
                    total = produtosSemCodigo.Count 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gera um código de barras seguindo as regras específicas do sistema
        /// Baseado na lógica do n8n que usa CodigoNFe, Tamanho e PrecoVenda
        /// </summary>
        /// <param name="empresaId">ID da empresa</param>
        /// <param name="produtoId">ID do produto</param>
        /// <returns>Código de barras gerado</returns>
        private string? GerarCodigoBarras(string empresaId, string produtoId)
        {
            try
            {
                // Buscar o produto completo no banco para obter CodigoNFe, PrecoVenda e Tamanho
                var produto = _db._repositoryProduto.Collection.Find(x => x.Id == produtoId)
                    .Project(new BsonDocument { 
                        { "CodigoNFe", true }, 
                        { "PrecoVenda", true }, 
                        { "Tamanho", true } 
                    }).FirstOrDefault();
                
                if (produto == null)
                {
                    return null;
                }

                var produtoObj = BsonSerializer.Deserialize<dynamic>(produto.ToJson());

                // Part1: CodigoNFe sem hífens
                string part1 = produtoObj.CodigoNFe?.ToString()?.Replace("-", "") ?? "";

                // Part2: Usar o campo Tamanho do produto ou "0" se não existir
                string part2 = "0";
                if (produtoObj.Tamanho != null)
                {
                    string tamanhoStr = produtoObj.Tamanho.ToString();
                    if (!string.IsNullOrEmpty(tamanhoStr) && int.TryParse(tamanhoStr, out int tamanhoValue))
                    {
                        part2 = tamanhoValue.ToString();
                    }
                }

                // Part3: PrecoVenda formatado (sem ponto decimal e sem vírgula)
                double precoVenda = produtoObj.PrecoVenda != null ? (double)produtoObj.PrecoVenda : 0.0;
                string part3 = precoVenda.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "");

                // Zeros fixos conforme regra
                string prefixZero = "0";      // Sempre começar com 0
                string middleZero1 = "0";     // Zero entre CodigoNFe e TAMANHO
                string middleZero2 = "0";     // Zero entre TAMANHO e PrecoVenda

                // Concatena os valores sem preenchimento extra
                string baseCode = prefixZero + part1 + middleZero1 + part2 + middleZero2 + part3;

                // Calcula quantos zeros precisam ser adicionados entre TAMANHO e PrecoVenda
                int totalLength = baseCode.Length;
                int zerosNeeded = 14 - totalLength;

                // Adiciona os zeros extras entre TAMANHO e PrecoVenda, se necessário
                string paddingZeros = zerosNeeded > 0 ? new string('0', zerosNeeded) : "";

                // Código final
                string finalCode = prefixZero + part1 + middleZero1 + part2 + paddingZeros + middleZero2 + part3;

                return finalCode.Trim();
            }
            catch (Exception ex)
            {
                // Log do erro se necessário
                Console.WriteLine($"Erro ao gerar código de barras: {ex.Message}");
                return null;
            }
        }

        [HttpPost]
        public string GetProduto(string deposito, string produto)
        {
            var produtoEscolhido = BsonSerializer.Deserialize<ProdutoEscolhido>(_db._repositoryProduto.Collection.Find(x => x.Id == produto).Project(new BsonDocument { 
                { "_id", true }, 
                { "CodigoNFe", true }, 
                { "Nome", true }, 
                { "PrecoVenda", true }, 
                { "Marca", true }, 
                { "NumeroSerie", true },
                { "EAN_NFe", true } 
            }).FirstOrDefault().ToJson());

            if (produtoEscolhido != null)
            {
                return "{" +
                    "\"Id\":\"" + produtoEscolhido.Id + "\"," +
                    "\"Codigo\":\"" + (produtoEscolhido.Codigo ?? "") + "\"," +
                    "\"CodigoBarras\":\"" + (produtoEscolhido.CodigoBarras ?? "") + "\"," +
                    //replace utilizado para tratar inserções no banco de dados de "
                    "\"Nome\":\"" + produtoEscolhido.Nome.Replace("\"","\\\"") + "\"," +
                    "\"PrecoVenda\":\"" + produtoEscolhido.Preco + "\"," +
                    "\"Marca\":\"" + (produtoEscolhido.Marca ?? "") + "\"," +
                    "\"NumeroSerie\":\"" + (produtoEscolhido.NumeroSerie ?? "") + "\"}";
            }
            return "Erro";
        }

        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
