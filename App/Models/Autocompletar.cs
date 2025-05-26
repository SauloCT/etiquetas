using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VendaERP.Core;
using Microsoft.Extensions.Logging;

namespace App.Models
{
    /// <summary>
    /// Interface para o serviço de autocompletar que fornece dados para componentes de UI
    /// </summary>
    public interface IAutocompletarService
    {
        /// <summary>
        /// Obtém lista de clientes de forma assíncrona
        /// </summary>
        Task<List<Cliente>> GetClientesAsync();
        
        /// <summary>
        /// Obtém lista de empresas de forma assíncrona
        /// </summary>
        Task<List<Empresa>> GetEmpresasAsync();
        
        /// <summary>
        /// Obtém lista de tabelas de preço de forma assíncrona
        /// </summary>
        Task<List<TabelaDePreco>> GetTabelaDePrecosAsync();
        
        /// <summary>
        /// Obtém lista de depósitos de forma assíncrona
        /// </summary>
        Task<List<Deposito>> GetDepositosAsync();
        
        /// <summary>
        /// Obtém lista de produtos de forma assíncrona
        /// </summary>
        Task<List<ProdutoAutocompletar>> GetProdutosAsync();
        
        /// <summary>
        /// Obtém lista de modelos de etiquetas de forma assíncrona
        /// </summary>
        Task<List<ModeloEtiqueta>> GetModelosEtiquetasAsync();
        
        /// <summary>
        /// Obtém todos os dados de autocompletar de forma otimizada (paralela)
        /// </summary>
        Task<Autocompletar> GetAutocompletarDataAsync();
        
        /// <summary>
        /// Limpa o cache de dados
        /// </summary>
        void ClearCache();
    }

    /// <summary>
    /// Serviço otimizado para fornecer dados de autocompletar com cache e execução paralela
    /// </summary>
    public class AutocompletarService : IAutocompletarService
    {
        private readonly DBAccess _dbAccess;
        private readonly ILogger<AutocompletarService> _logger;
        
        // Cache para evitar múltiplas consultas ao banco de dados
        private List<Cliente>? _clientesCache;
        private List<Empresa>? _empresasCache;
        private List<TabelaDePreco>? _tabelaDePrecosCache;
        private List<Deposito>? _depositosCache;
        private List<ProdutoAutocompletar>? _produtosCache;
        private List<ModeloEtiqueta>? _modelosEtiquetasCache;
        private DateTime _lastCacheUpdate = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        /// <summary>
        /// Construtor do serviço de autocompletar
        /// </summary>
        /// <param name="dbAccess">Instância de acesso ao banco de dados</param>
        /// <param name="logger">Logger para registrar eventos e erros</param>
        /// <exception cref="ArgumentNullException">Lançado quando dbAccess ou logger são nulos</exception>
        public AutocompletarService(DBAccess dbAccess, ILogger<AutocompletarService> logger)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtém lista de clientes com cache e deserialização direta
        /// </summary>
        public async Task<List<Cliente>> GetClientesAsync()
        {
            try
            {
                if (_clientesCache == null || IsCacheExpired())
                {
                    _logger.LogDebug("Carregando clientes do banco de dados");
                    
                    // Deserialização direta sem conversão JSON
                    var clientesBson = await Task.Run(() => 
                        _dbAccess._repositoryPessoa.Collection
                            .Aggregate()
                            .Project(new BsonDocument{{"_id", true},{"NomeFantasia", true},{"Email", true}})
                            .Sort("{NomeFantasia:1}")
                            .ToList());
                    
                    // Deserialização direta do BSON para objeto, sem conversão JSON
                    _clientesCache = clientesBson?.Any() == true 
                        ? clientesBson.Select(doc => BsonSerializer.Deserialize<Cliente>(doc)).ToList()
                        : new List<Cliente>();
                    
                    _logger.LogInformation("Carregados {Count} clientes", _clientesCache.Count);
                }
                
                return _clientesCache ?? new List<Cliente>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar clientes");
                return new List<Cliente>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar clientes");
                return new List<Cliente>();
            }
        }

        /// <summary>
        /// Obtém lista de empresas com cache e deserialização direta
        /// </summary>
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
                    
                    // Deserialização direta do BSON para objeto
                    _empresasCache = empresaBson?.Any() == true 
                        ? empresaBson.Select(doc => BsonSerializer.Deserialize<Empresa>(doc)).ToList()
                        : new List<Empresa>();
                    
                    _logger.LogInformation("Carregadas {Count} empresas", _empresasCache.Count);
                }
                
                return _empresasCache ?? new List<Empresa>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar empresas");
                return new List<Empresa>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar empresas");
                return new List<Empresa>();
            }
        }

        /// <summary>
        /// Obtém lista de tabelas de preço com cache e deserialização direta
        /// </summary>
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
                    
                    // Deserialização direta do BSON para objeto
                    _tabelaDePrecosCache = tabelaDePrecoBson?.Any() == true 
                        ? tabelaDePrecoBson.Select(doc => BsonSerializer.Deserialize<TabelaDePreco>(doc)).ToList()
                        : new List<TabelaDePreco>();
                    
                    _logger.LogInformation("Carregadas {Count} tabelas de preço", _tabelaDePrecosCache.Count);
                }
                
                return _tabelaDePrecosCache ?? new List<TabelaDePreco>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar tabelas de preço");
                return new List<TabelaDePreco>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar tabelas de preço");
                return new List<TabelaDePreco>();
            }
        }

        /// <summary>
        /// Obtém lista de depósitos com cache e deserialização direta
        /// </summary>
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
                    
                    // Deserialização direta do BSON para objeto
                    _depositosCache = depositoBson?.Any() == true 
                        ? depositoBson.Select(doc => BsonSerializer.Deserialize<Deposito>(doc)).ToList()
                        : new List<Deposito>();
                    
                    _logger.LogInformation("Carregados {Count} depósitos", _depositosCache.Count);
                }
                
                return _depositosCache ?? new List<Deposito>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar depósitos");
                return new List<Deposito>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar depósitos");
                return new List<Deposito>();
            }
        }

        /// <summary>
        /// Obtém lista de produtos com cache e deserialização direta
        /// </summary>
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
                    
                    // Deserialização direta do BSON para objeto
                    _produtosCache = produtosBson?.Any() == true 
                        ? produtosBson.Select(doc => BsonSerializer.Deserialize<ProdutoAutocompletar>(doc)).ToList()
                        : new List<ProdutoAutocompletar>();
                    
                    _logger.LogInformation("Carregados {Count} produtos", _produtosCache.Count);
                }
                
                return _produtosCache ?? new List<ProdutoAutocompletar>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar produtos");
                return new List<ProdutoAutocompletar>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar produtos");
                return new List<ProdutoAutocompletar>();
            }
        }

        /// <summary>
        /// Obtém lista de modelos de etiquetas com cache e deserialização direta
        /// </summary>
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
                    
                    // Deserialização direta do BSON para objeto
                    _modelosEtiquetasCache = modelosEtiquetasBson?.Any() == true 
                        ? modelosEtiquetasBson.Select(doc => BsonSerializer.Deserialize<ModeloEtiqueta>(doc)).ToList()
                        : new List<ModeloEtiqueta>();
                    
                    _logger.LogInformation("Carregados {Count} modelos de etiquetas", _modelosEtiquetasCache.Count);
                }
                
                return _modelosEtiquetasCache ?? new List<ModeloEtiqueta>();
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "Erro de conexão com MongoDB ao carregar modelos de etiquetas");
                return new List<ModeloEtiqueta>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao carregar modelos de etiquetas");
                return new List<ModeloEtiqueta>();
            }
        }

        /// <summary>
        /// Obtém todos os dados de autocompletar de forma otimizada usando execução paralela
        /// Esta implementação executa todas as consultas em paralelo para melhor performance
        /// </summary>
        public async Task<Autocompletar> GetAutocompletarDataAsync()
        {
            try
            {
                _logger.LogDebug("Carregando dados completos do autocompletar em paralelo");
                
                // Execução paralela de todas as consultas para melhor performance
                var clientesTask = GetClientesAsync();
                var empresasTask = GetEmpresasAsync();
                var tabelaDePrecosTask = GetTabelaDePrecosAsync();
                var depositosTask = GetDepositosAsync();
                var produtosTask = GetProdutosAsync();
                var modelosEtiquetasTask = GetModelosEtiquetasAsync();

                // Aguarda todas as tarefas completarem em paralelo
                await Task.WhenAll(clientesTask, empresasTask, tabelaDePrecosTask, 
                                 depositosTask, produtosTask, modelosEtiquetasTask);

                var autocompletar = new Autocompletar
                {
                    clientes = await clientesTask,
                    empresas = await empresasTask,
                    tabelaDePreco = await tabelaDePrecosTask,
                    depositos = await depositosTask,
                    produtos = await produtosTask,
                    modelosEtiquetas = await modelosEtiquetasTask
                };

                UpdateCacheTimestamp();
                
                _logger.LogInformation("Dados do autocompletar carregados com sucesso em paralelo");
                return autocompletar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar dados do autocompletar");
                return new Autocompletar(); // Retorna objeto vazio em caso de erro
            }
        }

        /// <summary>
        /// Verifica se o cache expirou baseado no tempo de expiração configurado
        /// </summary>
        /// <returns>True se o cache expirou, False caso contrário</returns>
        private bool IsCacheExpired()
        {
            return DateTime.Now - _lastCacheUpdate > _cacheExpiration;
        }

        /// <summary>
        /// Atualiza o timestamp do cache para o momento atual
        /// </summary>
        private void UpdateCacheTimestamp()
        {
            _lastCacheUpdate = DateTime.Now;
        }

        /// <summary>
        /// Limpa todo o cache forçando uma nova consulta na próxima requisição
        /// </summary>
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

    /// <summary>
    /// Classe que agrupa todos os dados necessários para componentes de autocompletar
    /// </summary>
    public class Autocompletar
    {
        /// <summary>
        /// Lista de clientes disponíveis para autocompletar
        /// </summary>
        public List<Cliente> clientes { get; set; } = new();
        
        /// <summary>
        /// Lista de empresas disponíveis para autocompletar
        /// </summary>
        public List<Empresa> empresas { get; set; } = new();
        
        /// <summary>
        /// Lista de tabelas de preço disponíveis para autocompletar
        /// </summary>
        public List<TabelaDePreco> tabelaDePreco { get; set; } = new();
        
        /// <summary>
        /// Lista de depósitos disponíveis para autocompletar
        /// </summary>
        public List<Deposito> depositos { get; set; } = new();
        
        /// <summary>
        /// Lista de produtos disponíveis para autocompletar
        /// </summary>
        public List<ProdutoAutocompletar> produtos { get; set; } = new();
        
        /// <summary>
        /// Lista de modelos de etiquetas disponíveis para autocompletar
        /// </summary>
        public List<ModeloEtiqueta> modelosEtiquetas { get; set; } = new();
        
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Autocompletar() { }
        
        /// <summary>
        /// Construtor legado mantido para compatibilidade (DEPRECATED)
        /// Recomenda-se usar o AutocompletarService para melhor performance
        /// </summary>
        /// <param name="db">Instância de acesso ao banco de dados</param>
        [Obsolete("Use AutocompletarService.GetAutocompletarDataAsync() para melhor performance")]
        public Autocompletar(DBAccess db)
        {
            try
            {
                // Implementação legada mantida para compatibilidade
                var clientesBson = db._repositoryPessoa.Collection.Aggregate().Project(new BsonDocument{{"_id", true},{"NomeFantasia", true},{"Email", true } }).Sort("{NomeFantasia:1}").ToList();
                if(clientesBson?.Any() == true)
                    clientes = clientesBson.Select(doc => BsonSerializer.Deserialize<Cliente>(doc)).ToList();
                    
                var empresaBson = db._repositoryEmpresa.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "NomeFantasia", true } }).Sort("{RazaoSocial:1}").ToList();
                if (empresaBson?.Any() == true)
                    this.empresas = empresaBson.Select(doc => BsonSerializer.Deserialize<Empresa>(doc)).ToList();
                    
                var tabelaDePrecoBson = db._repositoryProdutoTabelaPreco.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (tabelaDePrecoBson?.Any() == true)
                    this.tabelaDePreco = tabelaDePrecoBson.Select(doc => BsonSerializer.Deserialize<TabelaDePreco>(doc)).ToList();
                    
                var depositoBson = db._repositoryDeposito.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (depositoBson?.Any() == true)
                    this.depositos = depositoBson.Select(doc => BsonSerializer.Deserialize<Deposito>(doc)).ToList();
                    
                var produtosBson = db._repositoryProduto.Collection.Aggregate().Project(new BsonDocument { 
                    { "_id", true }, 
                    { "Nome", true },
                    { "CodigoNFe", true }
                }).Sort("{Nome:1}").ToList();
                if (produtosBson?.Any() == true)
                    this.produtos = produtosBson.Select(doc => BsonSerializer.Deserialize<ProdutoAutocompletar>(doc)).ToList();
                    
                var modelosEtiquetasBson = db._repositoryEtiquetasPadroes.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
                if (modelosEtiquetasBson?.Any() == true)
                    this.modelosEtiquetas = modelosEtiquetasBson.Select(doc => BsonSerializer.Deserialize<ModeloEtiqueta>(doc)).ToList();
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
    
    /// <summary>
    /// Representa um cliente para componentes de autocompletar
    /// </summary>
    public class Cliente
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; } = null!;
        
        [BsonElement("Email")]
        public string? Email { get; set; }
    }
    
    /// <summary>
    /// Representa uma empresa para componentes de autocompletar
    /// </summary>
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; } = null!;
    }
    
    /// <summary>
    /// Representa uma tabela de preço para componentes de autocompletar
    /// </summary>
    public class TabelaDePreco
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
    
    /// <summary>
    /// Representa um depósito para componentes de autocompletar
    /// </summary>
    public class Deposito
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
    
    /// <summary>
    /// Representa um produto para componentes de autocompletar
    /// </summary>
    public class ProdutoAutocompletar
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
        
        [BsonElement("CodigoNFe")]
        public string? CodigoNFe { get; set; }
    }
    
    /// <summary>
    /// Representa um modelo de etiqueta para componentes de autocompletar
    /// </summary>
    public class ModeloEtiqueta
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
    }
}
