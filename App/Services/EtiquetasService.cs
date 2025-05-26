using App.Models;
using App.Services.Interfaces;
using App.VendaERP.Core.Models;
using static App.Services.Interfaces.IEtiquetasService;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VendaERP.Core;
using VendaERP.Core.Models;

namespace App.Services
{
    /// <summary>
    /// Serviço de etiquetas que encapsula toda a lógica de negócio
    /// </summary>
    public class EtiquetasService : IEtiquetasService
    {
        private readonly DBAccess _dbAccess;
        private readonly IValidationService _validationService;
        private readonly ILogger<EtiquetasService> _logger;

        public EtiquetasService(
            DBAccess dbAccess, 
            IValidationService validationService,
            ILogger<EtiquetasService> logger)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DtoEtiquetasPadroes> CriarModeloAsync(DtoEtiquetasPadroes modelo)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de modelo de etiqueta: {Nome}", modelo?.Nome);

                if (modelo == null)
                {
                    _logger.LogWarning("Tentativa de criar modelo nulo");
                    throw new ArgumentNullException(nameof(modelo), "Modelo não pode ser nulo");
                }

                var validation = _validationService.ValidarModelo(modelo);
                if (!validation.IsValid)
                {
                    _logger.LogWarning("Validação falhou para modelo: {Nome}. Erros: {Erros}", 
                        modelo.Nome, string.Join(", ", validation.Errors));
                    throw new ArgumentException($"Dados inválidos: {validation.ErrorMessage}");
                }

                // Sanitizar campos de texto
                modelo.Nome = _validationService.SanitizeString(modelo.Nome ?? string.Empty);

                // Verificar se já existe um modelo com o mesmo nome
                var modeloExistente = await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection
                        .Find(x => x.Nome == modelo.Nome)
                        .FirstOrDefault());

                if (modeloExistente != null)
                {
                    _logger.LogWarning("Tentativa de criar modelo com nome duplicado: {Nome}", modelo.Nome);
                    throw new InvalidOperationException($"Já existe um modelo com o nome '{modelo.Nome}'");
                }

                await Task.Run(() => _dbAccess._repositoryEtiquetasPadroes.Collection.InsertOne(modelo));

                _logger.LogInformation("Modelo de etiqueta criado com sucesso. ID: {Id}, Nome: {Nome}", 
                    modelo.Id, modelo.Nome);
                
                return modelo;
            }
            catch (ArgumentException)
            {
                throw; // Re-throw validation errors
            }
            catch (InvalidOperationException)
            {
                throw; // Re-throw business logic errors
            }
            catch (MongoDB.Driver.MongoWriteException ex)
            {
                _logger.LogError(ex, "Erro de escrita no MongoDB ao criar modelo: {Nome}", modelo?.Nome);
                throw new InvalidOperationException("Erro ao salvar no banco de dados. Verifique se os dados estão corretos.", ex);
            }
            catch (MongoDB.Driver.MongoConnectionException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao criar modelo: {Nome}", modelo?.Nome);
                throw new InvalidOperationException("Erro de conexão com o banco de dados. Tente novamente.", ex);
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Erro do MongoDB ao criar modelo: {Nome}", modelo?.Nome);
                throw new InvalidOperationException("Erro no banco de dados. Tente novamente.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar modelo de etiqueta: {Nome}", modelo?.Nome);
                throw new InvalidOperationException("Erro interno do servidor. Tente novamente.", ex);
            }
        }

        public async Task<DtoEtiquetasPadroes?> AtualizarModeloAsync(string id, DtoEtiquetasPadroes modelo)
        {
            try
            {
                _logger.LogInformation("Iniciando atualização de modelo de etiqueta. ID: {Id}", id);

                var idValidation = _validationService.ValidarId(id, "ID do modelo");
                if (!idValidation.IsValid)
                {
                    throw new ArgumentException(idValidation.ErrorMessage);
                }

                var modelValidation = _validationService.ValidarModelo(modelo);
                if (!modelValidation.IsValid)
                {
                    throw new ArgumentException($"Dados inválidos: {modelValidation.ErrorMessage}");
                }

                // Verificar se o modelo existe
                var modeloExistente = await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == id).FirstOrDefault());

                if (modeloExistente == null)
                {
                    _logger.LogWarning("Modelo de etiqueta não encontrado. ID: {Id}", id);
                    return null;
                }

                // Sanitizar campos de texto
                modelo.Nome = _validationService.SanitizeString(modelo.Nome ?? string.Empty);
                modelo.Id = id; // Garantir que o ID seja mantido

                await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection.ReplaceOne(x => x.Id == id, modelo));

                _logger.LogInformation("Modelo de etiqueta atualizado com sucesso. ID: {Id}", id);
                return modelo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar modelo de etiqueta. ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> RemoverModeloAsync(string id)
        {
            try
            {
                _logger.LogInformation("Iniciando remoção de modelo de etiqueta. ID: {Id}", id);

                var validation = _validationService.ValidarId(id, "ID do modelo");
                if (!validation.IsValid)
                {
                    throw new ArgumentException(validation.ErrorMessage);
                }

                var result = await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection.DeleteOne(x => x.Id == id));

                var sucesso = result.DeletedCount > 0;
                
                if (sucesso)
                {
                    _logger.LogInformation("Modelo de etiqueta removido com sucesso. ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Modelo de etiqueta não encontrado para remoção. ID: {Id}", id);
                }

                return sucesso;
            }
            catch (ArgumentException)
            {
                throw; // Re-throw validation errors
            }
            catch (MongoDB.Driver.MongoWriteException ex)
            {
                _logger.LogError(ex, "Erro de escrita no MongoDB ao remover modelo. ID: {Id}", id);
                throw new InvalidOperationException("Erro ao remover do banco de dados.", ex);
            }
            catch (MongoDB.Driver.MongoConnectionException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao remover modelo. ID: {Id}", id);
                throw new InvalidOperationException("Erro de conexão com o banco de dados. Tente novamente.", ex);
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Erro do MongoDB ao remover modelo. ID: {Id}", id);
                throw new InvalidOperationException("Erro no banco de dados. Tente novamente.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover modelo de etiqueta. ID: {Id}", id);
                throw new InvalidOperationException("Erro interno do servidor. Tente novamente.", ex);
            }
        }

        public async Task<DtoEtiquetasPadroes?> ObterModeloPorIdAsync(string id)
        {
            try
            {
                _logger.LogDebug("Buscando modelo de etiqueta. ID: {Id}", id);

                var validation = _validationService.ValidarId(id, "ID do modelo");
                if (!validation.IsValid)
                {
                    throw new ArgumentException(validation.ErrorMessage);
                }

                var modelo = await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == id).FirstOrDefault());

                if (modelo == null)
                {
                    _logger.LogWarning("Modelo de etiqueta não encontrado. ID: {Id}", id);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar modelo de etiqueta. ID: {Id}", id);
                throw;
            }
        }

        public async Task<(List<DtoEtiquetasPadroes> modelos, int totalCount)> ListarModelosAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogDebug("Listando modelos de etiqueta. Página: {PageNumber}, Tamanho: {PageSize}", pageNumber, pageSize);

                var validation = _validationService.ValidarPaginacao(pageNumber, pageSize);
                if (!validation.IsValid)
                {
                    throw new ArgumentException(validation.ErrorMessage);
                }

                var totalCount = await Task.Run(() => 
                    (int)_dbAccess._repositoryEtiquetasPadroes.Collection.CountDocuments(x => true));

                var modelos = await Task.Run(() => 
                    _dbAccess._repositoryEtiquetasPadroes.Collection
                        .Find(x => true)
                        .Sort("{_id: -1}")
                        .Skip((pageNumber - 1) * pageSize)
                        .Limit(pageSize)
                        .ToList());

                _logger.LogDebug("Encontrados {Count} modelos de etiqueta", modelos.Count);
                return (modelos, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar modelos de etiqueta");
                throw;
            }
        }

        public async Task<EtiquetasProcessResult> ProcessarEtiquetasAsync(EtiquetasRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando processamento de etiquetas para empresa: {Empresa}", request.Empresa);

                var validation = _validationService.ValidarEtiquetasRequest(request);
                if (!validation.IsValid)
                {
                    throw new ArgumentException($"Dados inválidos: {validation.ErrorMessage}");
                }

                // Buscar dados da empresa
                var nomeEmpresa = await ObterNomeEmpresaAsync(request.Empresa);
                
                // Buscar modelo de etiqueta
                var modelEtiqueta = await ObterModeloPorIdAsync(request.Etiqueta);
                if (modelEtiqueta == null)
                {
                    throw new InvalidOperationException("Modelo de etiqueta não encontrado");
                }

                // Processar itens
                var listaItens = await ProcessarItensAsync(request.Itens, request.GerarCodigosBarras, request.Empresa);

                // Criar opções selecionadas
                var opcoesSelecionadas = new OpcoesSelecionadas
                {
                    DadoLadoCodigoBarras = request.DadoLadoCodigoBarras,
                    ImprimirBorda = request.ImprimirBorda,
                    ImprimirCodigo = request.ImprimirCodigo,
                    ImprimirCodigoBarras = request.ImprimirCodigoBarras,
                    ImprimirLote = request.ImprimirLote,
                    ImprimirMarca = request.ImprimirMarca,
                    ImprimirNome = request.ImprimirNome,
                    ImprimirNumeroCodigoBarras = request.ImprimirNumeroCodigoBarras,
                    ImprimirNumeroSerie = request.ImprimirNumeroSerie,
                    ImprimirPreco = request.ImprimirPreco,
                    PrecoComoCodigo = request.PrecoComoCodigo,
                    GerarCodigosBarras = request.GerarCodigosBarras
                };

                _logger.LogInformation("Processamento de etiquetas concluído. {Count} itens processados", listaItens.Count);

                return new EtiquetasProcessResult
                {
                    NomeEmpresa = nomeEmpresa,
                    ModelEtiqueta = modelEtiqueta,
                    OpcoesSelecionadas = opcoesSelecionadas,
                    ListaItens = listaItens
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar etiquetas");
                throw;
            }
        }

        public async Task<List<ProdutoSemCodigo>> VerificarProdutosSemCodigoAsync(string[] itens)
        {
            try
            {
                _logger.LogDebug("Verificando produtos sem código de barras. {Count} itens", itens?.Length ?? 0);

                var validation = _validationService.ValidarItens(itens);
                if (!validation.IsValid)
                {
                    throw new ArgumentException(validation.ErrorMessage);
                }

                var produtosSemCodigo = new List<ProdutoSemCodigo>();

                foreach (var item in itens)
                {
                    var parts = item.Split(',');
                    var produtoId = parts[0];

                    var produto = await Task.Run(() => 
                        _dbAccess._repositoryProduto.Collection.Find(x => x.Id == produtoId)
                            .Project(new BsonDocument { 
                                { "_id", true }, 
                                { "Nome", true }, 
                                { "EAN_NFe", true } 
                            }).FirstOrDefault());

                    if (produto != null)
                    {
                        var id = produto["_id"].ToString();
                        var nome = "Nome não encontrado";
                        var eanNfe = "";

                        if (produto.Contains("Nome") && produto["Nome"] != null && !produto["Nome"].IsBsonNull)
                        {
                            nome = produto["Nome"].ToString();
                        }

                        if (produto.Contains("EAN_NFe") && produto["EAN_NFe"] != null && !produto["EAN_NFe"].IsBsonNull)
                        {
                            eanNfe = produto["EAN_NFe"].ToString();
                        }

                        if (string.IsNullOrEmpty(eanNfe))
                        {
                            produtosSemCodigo.Add(new ProdutoSemCodigo
                            {
                                Id = id,
                                Nome = nome
                            });
                        }
                    }
                }

                _logger.LogDebug("Encontrados {Count} produtos sem código de barras", produtosSemCodigo.Count);
                return produtosSemCodigo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar produtos sem código");
                throw;
            }
        }

        public async Task<ProdutoEscolhido?> ObterProdutoAsync(string deposito, string produto)
        {
            try
            {
                _logger.LogDebug("Buscando produto. ID: {Produto}, Depósito: {Deposito}", produto, deposito);

                if (string.IsNullOrWhiteSpace(produto))
                {
                    _logger.LogWarning("ID do produto está vazio ou nulo");
                    throw new ArgumentException("ID do produto é obrigatório", nameof(produto));
                }

                var validation = _validationService.ValidarId(produto, "ID do produto");
                if (!validation.IsValid)
                {
                    _logger.LogWarning("ID do produto inválido: {Produto}", produto);
                    throw new ArgumentException(validation.ErrorMessage);
                }

                var produtoDocument = await Task.Run(() => 
                    _dbAccess._repositoryProduto.Collection.Find(x => x.Id == produto)
                        .Project(new BsonDocument { 
                            { "_id", true }, 
                            { "CodigoNFe", true }, 
                            { "Nome", true }, 
                            { "PrecoVenda", true }, 
                            { "Marca", true }, 
                            { "NumeroSerie", true },
                            { "EAN_NFe", true } 
                        }).FirstOrDefault());

                if (produtoDocument == null)
                {
                    _logger.LogWarning("Produto não encontrado. ID: {Produto}", produto);
                    return null;
                }

                var produtoEscolhido = BsonSerializer.Deserialize<ProdutoEscolhido>(produtoDocument.ToJson());
                
                _logger.LogDebug("Produto encontrado com sucesso. ID: {Produto}, Nome: {Nome}", 
                    produto, produtoEscolhido.Nome);
                
                return produtoEscolhido;
            }
            catch (ArgumentException)
            {
                throw; // Re-throw validation errors
            }
            catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError(ex, "Erro do MongoDB ao buscar produto. ID: {Produto}", produto);
                throw new InvalidOperationException("Erro de conexão com o banco de dados. Tente novamente.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao buscar produto. ID: {Produto}", produto);
                throw new InvalidOperationException("Erro interno do servidor. Tente novamente.", ex);
            }
        }

        #region Métodos Privados

        private async Task<string> ObterNomeEmpresaAsync(string empresaId)
        {
            try
            {
                var empresa = await Task.Run(() => 
                    _dbAccess._repositoryEmpresa.Collection.Find(x => x.Id == empresaId).FirstOrDefault());

                if (empresa == null)
                {
                    throw new InvalidOperationException("Empresa não encontrada");
                }

                return empresa.NomeFantasia ?? "Nome não informado";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresa. ID: {EmpresaId}", empresaId);
                throw;
            }
        }

        private async Task<List<ProdutoEscolhido>> ProcessarItensAsync(string[] itens, bool gerarCodigosBarras, string empresaId)
        {
            var listaItens = new List<ProdutoEscolhido>();

            foreach (var item in itens)
            {
                var parts = item.Split(',');
                var produtoId = parts[0];
                var quantidade = int.Parse(parts[1]);
                var lote = parts.Length > 2 ? parts[2] : null;
                var numeroSerie = parts.Length > 3 ? parts[3] : null;

                var produtoDocument = await Task.Run(() => 
                    _dbAccess._repositoryProduto.Collection.Find(x => x.Id == produtoId)
                        .Project(new BsonDocument { 
                            { "_id", true }, 
                            { "CodigoNFe", true }, 
                            { "Nome", true }, 
                            { "PrecoVenda", true }, 
                            { "Marca", true }, 
                            { "EAN_NFe", true },
                            { "Tamanho", true }
                        }).FirstOrDefault());

                if (produtoDocument != null)
                {
                    var produto = BsonSerializer.Deserialize<ProdutoEscolhido>(produtoDocument.ToJson());
                    produto.Quantidade = quantidade;
                    
                    if (!string.IsNullOrEmpty(lote))
                        produto.Lote = lote;
                    
                    if (!string.IsNullOrEmpty(numeroSerie))
                        produto.NumeroSerie = numeroSerie;

                    // Gerar código de barras se necessário
                    if (gerarCodigosBarras && string.IsNullOrEmpty(produto.CodigoBarras))
                    {
                        var novoCodigoBarras = await GerarCodigoBarrasAsync(empresaId, produto.Id);
                        if (!string.IsNullOrEmpty(novoCodigoBarras))
                        {
                            produto.CodigoBarras = novoCodigoBarras;
                            
                            // Atualizar no banco de dados
                            await AtualizarCodigoBarrasProdutoAsync(produto.Id, novoCodigoBarras);
                        }
                    }

                    listaItens.Add(produto);
                }
            }

            return listaItens;
        }

        private async Task<string?> GerarCodigoBarrasAsync(string empresaId, string produtoId)
        {
            try
            {
                var produto = await Task.Run(() => 
                    _dbAccess._repositoryProduto.Collection.Find(x => x.Id == produtoId)
                        .Project(new BsonDocument { 
                            { "CodigoNFe", true }, 
                            { "PrecoVenda", true }, 
                            { "Tamanho", true } 
                        }).FirstOrDefault());

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

                // Part3: PrecoVenda formatado
                double precoVenda = produtoObj.PrecoVenda != null ? (double)produtoObj.PrecoVenda : 0.0;
                string part3 = precoVenda.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "");

                // Construir código final
                string prefixZero = "0";
                string middleZero1 = "0";
                string middleZero2 = "0";

                string baseCode = prefixZero + part1 + middleZero1 + part2 + middleZero2 + part3;
                int totalLength = baseCode.Length;
                int zerosNeeded = 14 - totalLength;

                string paddingZeros = zerosNeeded > 0 ? new string('0', zerosNeeded) : "";
                string finalCode = prefixZero + part1 + middleZero1 + part2 + paddingZeros + middleZero2 + part3;

                return finalCode.Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar código de barras para produto: {ProdutoId}", produtoId);
                return null;
            }
        }

        private async Task AtualizarCodigoBarrasProdutoAsync(string produtoId, string codigoBarras)
        {
            try
            {
                // Validação de entrada para prevenir injection
                if (string.IsNullOrWhiteSpace(produtoId) || !ObjectId.TryParse(produtoId, out ObjectId objectId))
                {
                    throw new ArgumentException("ID do produto inválido", nameof(produtoId));
                }

                var filter = Builders<DtoProduto>.Filter.Eq("_id", objectId);
                var update = Builders<DtoProduto>.Update.Set("EAN_NFe", codigoBarras);
                
                await Task.Run(() => _dbAccess._repositoryProduto.Collection.UpdateOne(filter, update));
                
                _logger.LogDebug("Código de barras atualizado para produto: {ProdutoId}", produtoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar código de barras do produto: {ProdutoId}", produtoId);
                throw;
            }
        }

        #endregion
    }
} 