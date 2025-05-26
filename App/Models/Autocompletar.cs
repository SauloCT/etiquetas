using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VendaERP.Core;
using Microsoft.Extensions.Logging;

namespace App.Models
{
    public interface IAutocompletarService
    {
        Task<List<Cliente>> GetClientesAsync();
        Task<List<Empresa>> GetEmpresasAsync();
        Task<List<TabelaDePreco>> GetTabelaDePrecosAsync();
        Task<List<Deposito>> GetDepositosAsync();
        Task<List<ProdutoAutocompletar>> GetProdutosAsync();
        Task<List<ModeloEtiqueta>> GetModelosEtiquetasAsync();
        Task<Autocompletar> GetAutocompletarDataAsync();
        void ClearCache();
    }

    public class AutocompletarService : IAutocompletarService
    {
        private readonly DBAccess _dbAccess;
        private readonly ILogger<AutocompletarService> _logger;
        
        // Cache para evitar múltiplas consultas
        private List<Cliente>? _clientesCache;
        private List<Empresa>? _empresasCache;
        private List<TabelaDePreco>? _tabelaDePrecosCache;
        private List<Deposito>? _depositosCache;
        private List<ProdutoAutocompletar>? _produtosCache;
        private List<ModeloEtiqueta>? _modelosEtiquetasCache;
        private DateTime _lastCacheUpdate = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        public AutocompletarService(DBAccess dbAccess, ILogger<AutocompletarService> logger)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            try
            {
                if (_clientesCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando clientes do banco de dados");
                    
                    var clientesBson = await Task.Run(() => 
                        _dbAccess._repositoryPessoa.Collection
                            .Aggregate()
                            .Project(new BsonDocument{{"_id", true},{"NomeFantasia", true},{"Email", true}})
                            .Sort("{NomeFantasia:1}")
                            .ToList());
                    
                    _clientesCache = clientesBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<Cliente>>(clientesBson.ToJson())
                        : new List<Cliente>();
                    
                    _logger.LogInformation("Carregados {Count} clientes", _clientesCache.Count);
                }
                
                return _clientesCache ?? new List<Cliente>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar clientes");
                return new List<Cliente>();
            }
        }

        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            try
            {
                if (_empresasCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando empresas do banco de dados");
                    
                    var empresaBson = await Task.Run(() => 
                        _dbAccess._repositoryEmpresa.Collection
                            .Aggregate()
                            .Project(new BsonDocument { { "_id", true }, { "NomeFantasia", true } })
                            .Sort("{RazaoSocial:1}")
                            .ToList());
                    
                    _empresasCache = empresaBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<Empresa>>(empresaBson.ToJson())
                        : new List<Empresa>();
                    
                    _logger.LogInformation("Carregadas {Count} empresas", _empresasCache.Count);
                }
                
                return _empresasCache ?? new List<Empresa>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar empresas");
                return new List<Empresa>();
            }
        }

        public async Task<List<TabelaDePreco>> GetTabelaDePrecosAsync()
        {
            try
            {
                if (_tabelaDePrecosCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando tabelas de preço do banco de dados");
                    
                    var tabelaDePrecoBson = await Task.Run(() => 
                        _dbAccess._repositoryProdutoTabelaPreco.Collection
                            .Aggregate()
                            .Project(new BsonDocument { { "_id", true }, { "Nome", true } })
                            .Sort("{Nome:1}")
                            .ToList());
                    
                    _tabelaDePrecosCache = tabelaDePrecoBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<TabelaDePreco>>(tabelaDePrecoBson.ToJson())
                        : new List<TabelaDePreco>();
                    
                    _logger.LogInformation("Carregadas {Count} tabelas de preço", _tabelaDePrecosCache.Count);
                }
                
                return _tabelaDePrecosCache ?? new List<TabelaDePreco>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar tabelas de preço");
                return new List<TabelaDePreco>();
            }
        }

        public async Task<List<Deposito>> GetDepositosAsync()
        {
            try
            {
                if (_depositosCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando depósitos do banco de dados");
                    
                    var depositoBson = await Task.Run(() => 
                        _dbAccess._repositoryDeposito.Collection
                            .Aggregate()
                            .Project(new BsonDocument { { "_id", true }, { "Nome", true } })
                            .Sort("{Nome:1}")
                            .ToList());
                    
                    _depositosCache = depositoBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<Deposito>>(depositoBson.ToJson())
                        : new List<Deposito>();
                    
                    _logger.LogInformation("Carregados {Count} depósitos", _depositosCache.Count);
                }
                
                return _depositosCache ?? new List<Deposito>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar depósitos");
                return new List<Deposito>();
            }
        }

        public async Task<List<ProdutoAutocompletar>> GetProdutosAsync()
        {
            try
            {
                if (_produtosCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando produtos do banco de dados");
                    
                    var produtosBson = await Task.Run(() => 
                        _dbAccess._repositoryProduto.Collection
                            .Aggregate()
                            .Project(new BsonDocument { 
                                { "_id", true }, 
                                { "Nome", true },
                                { "CodigoNFe", true }
                            })
                            .Sort("{Nome:1}")
                            .ToList());
                    
                    _produtosCache = produtosBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<ProdutoAutocompletar>>(produtosBson.ToJson())
                        : new List<ProdutoAutocompletar>();
                    
                    _logger.LogInformation("Carregados {Count} produtos", _produtosCache.Count);
                }
                
                return _produtosCache ?? new List<ProdutoAutocompletar>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar produtos");
                return new List<ProdutoAutocompletar>();
            }
        }

        public async Task<List<ModeloEtiqueta>> GetModelosEtiquetasAsync()
        {
            try
            {
                if (_modelosEtiquetasCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando modelos de etiquetas do banco de dados");
                    
                    var modelosEtiquetasBson = await Task.Run(() => 
                        _dbAccess._repositoryEtiquetasPadroes.Collection
                            .Aggregate()
                            .Project(new BsonDocument { { "_id", true }, { "Nome", true } })
                            .Sort("{Nome:1}")
                            .ToList());
                    
                    _modelosEtiquetasCache = modelosEtiquetasBson?.Any() == true 
                        ? BsonSerializer.Deserialize<List<ModeloEtiqueta>>(modelosEtiquetasBson.ToJson())
                        : new List<ModeloEtiqueta>();
                    
                    _logger.LogInformation("Carregados {Count} modelos de etiquetas", _modelosEtiquetasCache.Count);
                }
                
                return _modelosEtiquetasCache ?? new List<ModeloEtiqueta>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar modelos de etiquetas");
                return new List<ModeloEtiqueta>();
            }
        }

        public async Task<Autocompletar> GetAutocompletarDataAsync()
        {
            try
            {
                _logger.LogDebug("Carregando dados completos do autocompletar");
                
                var autocompletar = new Autocompletar
                {
                    clientes = await GetClientesAsync(),
                    empresas = await GetEmpresasAsync(),
                    tabelaDePreco = await GetTabelaDePrecosAsync(),
                    depositos = await GetDepositosAsync(),
                    produtos = await GetProdutosAsync(),
                    modelosEtiquetas = await GetModelosEtiquetasAsync()
                };

                UpdateCacheTimestamp();
                
                _logger.LogInformation("Dados do autocompletar carregados com sucesso");
                return autocompletar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar dados do autocompletar");
                return new Autocompletar(); // Retorna objeto vazio em caso de erro
            }
        }

        private bool IsCacheExpired()
        {
            return DateTime.Now - _lastCacheUpdate > _cacheExpiration;
        }

        private void UpdateCacheTimestamp()
        {
            _lastCacheUpdate = DateTime.Now;
        }

        public void ClearCache()
        {
            _logger.LogInformation("Limpando cache do autocompletar");
            _clientesCache = null;
            _empresasCache = null;
            _tabelaDePrecosCache = null;
            _depositosCache = null;
            _produtosCache = null;
            _modelosEtiquetasCache = null;
            _lastCacheUpdate = DateTime.MinValue;
        }
    }

    public class Autocompletar
    {
        /// <summary>
        /// Lista de clientes
        /// </summary>
        public List<Cliente> clientes { get; set; } = new();
        /// <summary>
        /// Lista de empresas
        /// </summary>
        public List<Empresa> empresas { get; set; } = new();
        public List<TabelaDePreco> tabelaDePreco { get; set; } = new();
        public List<Deposito> depositos { get; set; } = new();
        public List<ProdutoAutocompletar> produtos { get; set; } = new();
        public List<ModeloEtiqueta> modelosEtiquetas { get; set; } = new();
        
        // Construtor vazio para compatibilidade
        public Autocompletar() { }
        
        // Construtor original melhorado com tratamento de exceções
        public Autocompletar(DBAccess db)
        {
            var _db = db;
            
            try
            {
                var clientesBson = _db._repositoryPessoa.Collection.Aggregate().Project(new BsonDocument{{"_id", true},{"NomeFantasia", true},{"Email", true } }).Sort("{NomeFantasia:1}").ToList();
                if(clientesBson != null)
                    clientes = BsonSerializer.Deserialize<List<Cliente>>(clientesBson.ToJson());
                    
                var empresaBson = _db._repositoryEmpresa.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "NomeFantasia", true } }).Sort("{RazaoSocial:1}").ToList();
                if (empresaBson != null)
                    this.empresas = BsonSerializer.Deserialize<List<Empresa>>(empresaBson.ToJson());
                    
                var tabelaDePrecoBson = _db._repositoryProdutoTabelaPreco.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (tabelaDePrecoBson != null)
                    this.tabelaDePreco = BsonSerializer.Deserialize<List<TabelaDePreco>>(tabelaDePrecoBson.ToJson());
                    
                var depositoBson = _db._repositoryDeposito.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (depositoBson != null)
                    this.depositos = BsonSerializer.Deserialize<List<Deposito>>(depositoBson.ToJson());
                    
                var produtosBson = _db._repositoryProduto.Collection.Aggregate().Project(new BsonDocument { 
                    { "_id", true }, 
                    { "Nome", true },
                    { "CodigoNFe", true }
                }).Sort("{Nome:1}").ToList();
                if (produtosBson != null)
                    this.produtos = BsonSerializer.Deserialize<List<ProdutoAutocompletar>>(produtosBson.ToJson());
                    
                var modelosEtiquetasBson = _db._repositoryEtiquetasPadroes.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (modelosEtiquetasBson != null)
                    this.modelosEtiquetas = BsonSerializer.Deserialize<List<ModeloEtiqueta>>(modelosEtiquetasBson.ToJson());
            }
            catch (Exception)
            {
                // Em caso de erro, manter listas vazias
                clientes = new List<Cliente>();
                empresas = new List<Empresa>();
                tabelaDePreco = new List<TabelaDePreco>();
                depositos = new List<Deposito>();
                produtos = new List<ProdutoAutocompletar>();
                modelosEtiquetas = new List<ModeloEtiqueta>();
            }
        }
    }
    
    public class Cliente
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; } = null!;
        [BsonElement("Email")]
        public string? Email { get; set; }
    }
    
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; } = null!;
    }
    
    public class TabelaDePreco
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
    
    public class Deposito
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
    
    public class ProdutoAutocompletar
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
        [BsonElement("CodigoNFe")]
        public string? CodigoNFe { get; set; }
    }
    
    public class ModeloEtiqueta
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
}
