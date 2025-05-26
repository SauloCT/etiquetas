using MongoDB.Driver;
using VendaERP.Core.Models;
using System.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using App.VendaERP.Core.Models;
using Microsoft.Extensions.Logging;

namespace VendaERP.Core
{
    public enum DBAcessReadMode
    {
        Primary = 1,
        Secondary = 2
    }

    public interface IDBAccess
    {
        IRepository<T> CreateRepository<T>() where T : IEntity;
        IRepositoryLastUpdate<T> CreateRepositoryLastUpdate<T>() where T : IEntityLastUpdate;
        bool TestConnection();
    }

    public class DBAccess : IDBAccess, IDisposable
    {
        private IMongoDatabase MongoDatabase;
        private MongoClient? _mongoClient;
        private Hashtable _repositories = null!;
        private bool _disposed;
        private readonly ILogger<DBAccess>? _logger;
        private readonly DBSettings _dbSettings;

        public bool IsReadOnlyConnection { get; set; }

        private void CheckDispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                    _mongoClient = null;
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Construtores

        private void CreateHash()
        {
            if (_repositories == null)
                _repositories = new Hashtable();
        }

        public DBAccess(IOptions<DBSettings> dbSettings, ILogger<DBAccess>? logger = null)
        {
            _logger = logger;
            _dbSettings = dbSettings?.Value ?? throw new ArgumentNullException(nameof(dbSettings));
            
            try
            {
                _logger?.LogInformation("Inicializando conexão com MongoDB");
                
                ConventionPack conventionPack = new ConventionPack();
                conventionPack.Add(new IgnoreExtraElementsConvention(ignoreExtraElements: true));
                ConventionRegistry.Register("SIGE Conventions", conventionPack, (Type t) => true);

                var mongoClientSettings = CreateMongoClientSettings();
                _mongoClient = new MongoClient(mongoClientSettings);
                
                var dataBase = _mongoClient.GetDatabase(_dbSettings.DBName);
                MongoDatabase = dataBase;
                CreateHash();
                
                // Testar conexão
                if (!TestConnection())
                {
                    throw new InvalidOperationException("Não foi possível estabelecer conexão com o banco de dados");
                }
                
                // Inicializar repositórios
                InitializeRepositories();
                
                _logger?.LogInformation("Conexão com MongoDB estabelecida com sucesso");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao inicializar conexão com MongoDB");
                throw;
            }
        }

        private MongoClientSettings CreateMongoClientSettings()
        {
            var stringConnection = string.Format("mongodb+srv://{0}:{1}@{2}/{3}?readPreference=primaryPreferred&ssl=false",
                _dbSettings.DBUser,
                _dbSettings.DBPassword,
                _dbSettings.DbHost,
                _dbSettings.DBName);

            var settings = MongoClientSettings.FromConnectionString(stringConnection);
            
            // Configurar timeouts
            settings.ConnectTimeout = TimeSpan.FromSeconds(30);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);
            settings.SocketTimeout = TimeSpan.FromSeconds(60);
            
            // Configurar retry writes
            settings.RetryWrites = true;
            settings.RetryReads = true;
            
            return settings;
        }

        private void InitializeRepositories()
        {
            try
            {
                _logger?.LogDebug("Inicializando repositórios essenciais");
                
                // Inicializar repositórios
                _repositoryEtiquetasPadroes = new MongoRepository<DtoEtiquetasPadroes>(MongoDatabase);
                _repositoryProduto = new MongoRepositoryLastUpdate<DtoProduto>(MongoDatabase);
                _repositoryEmpresa = new MongoRepositoryLastUpdate<DtoEmpresa>(MongoDatabase);
                _repositoryPessoa = new MongoRepositoryLastUpdate<DtoPessoa>(MongoDatabase);
                _repositoryProdutoTabelaPreco = new MongoRepositoryLastUpdate<DtoProdutoTabela>(MongoDatabase);
                _repositoryDeposito = new MongoRepositoryLastUpdate<DtoEstoqueDeposito>(MongoDatabase);
                
                // Inicializar repositórios com atributos
                _repositoryEtiquetasPadroesAtributo = new MongoRepository<DtoEtiquetasPadroes>(MongoDatabase);
                _repositoryProdutoAtributo = new MongoRepositoryLastUpdate<DtoProduto>(MongoDatabase);
                _repositoryEmpresaAtributo = new MongoRepositoryLastUpdate<DtoEmpresa>(MongoDatabase);
                _repositoryPessoaAtributo = new MongoRepositoryLastUpdate<DtoPessoa>(MongoDatabase);
                _repositoryProdutoTabelaPrecoAtributo = new MongoRepositoryLastUpdate<DtoProdutoTabela>(MongoDatabase);
                _repositoryDepositoAtributo = new MongoRepositoryLastUpdate<DtoEstoqueDeposito>(MongoDatabase);
                
                _logger?.LogDebug("Repositórios inicializados com sucesso");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao inicializar repositórios");
                throw;
            }
        }

        public bool TestConnection()
        {
            try
            {
                _logger?.LogDebug("Testando conexão com o banco de dados");
                
                // Tentar listar as coleções para verificar se a conexão está funcionando
                var collections = MongoDatabase.ListCollections().ToList();
                
                _logger?.LogDebug("Conexão testada com sucesso. {CollectionCount} coleções encontradas", collections.Count);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Falha no teste de conexão com o banco de dados");
                return false;
            }
        }

        #endregion

        #region Mongo Repositorios Getters

        public IRepository<T> CreateRepository<T>() where T : IEntity
        {
            CheckDispose();

            try
            {
                var typeId = typeof(T).FullName?.GetHashCode() ?? 0;

                if (!_repositories.ContainsKey(typeId))
                {
                    _logger?.LogDebug("Criando repositório para tipo {Type}", typeof(T).Name);
                    
                    var repositoryInstance = (MongoRepository<T>?)Activator.CreateInstance(typeof(MongoRepository<T>), MongoDatabase);
                    if (repositoryInstance != null)
                    {
                        _repositories.Add(typeId, repositoryInstance);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Não foi possível criar repositório para o tipo {typeof(T).Name}");
                    }
                }

                return (MongoRepository<T>)(_repositories[typeId] ?? throw new InvalidOperationException("Repository not found"));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao criar repositório para tipo {Type}", typeof(T).Name);
                throw;
            }
        }

        public IRepositoryLastUpdate<T> CreateRepositoryLastUpdate<T>() where T : IEntityLastUpdate
        {
            CheckDispose();

            try
            {
                var typeId = typeof(T).FullName?.GetHashCode() ?? 0;

                if (!_repositories.ContainsKey(typeId))
                {
                    _logger?.LogDebug("Criando repositório LastUpdate para tipo {Type}", typeof(T).Name);
                    
                    var repositoryInstance = (MongoRepositoryLastUpdate<T>?)Activator.CreateInstance(typeof(MongoRepositoryLastUpdate<T>), MongoDatabase);
                    if (repositoryInstance != null)
                    {
                        _repositories.Add(typeId, repositoryInstance);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Não foi possível criar repositório LastUpdate para o tipo {typeof(T).Name}");
                    }
                }

                return (MongoRepositoryLastUpdate<T>)(_repositories[typeId] ?? throw new InvalidOperationException("Repository not found"));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao criar repositório LastUpdate para tipo {Type}", typeof(T).Name);
                throw;
            }
        }

        public MongoRepository<T> GetRepositoryFromType<T>() where T : IEntity
        {
            try
            {
                return new MongoRepository<T>(MongoDatabase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao obter repositório para tipo {Type}", typeof(T).Name);
                throw;
            }
        }

        public MongoRepositoryLastUpdate<T> GetRepositoryLastUpdateFromType<T>() where T : IEntityLastUpdate
        {
            try
            {
                return new MongoRepositoryLastUpdate<T>(MongoDatabase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao obter repositório LastUpdate para tipo {Type}", typeof(T).Name);
                throw;
            }
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            try
            {
                return MongoDatabase.GetCollection<TEntity>(name);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao obter coleção {CollectionName}", name);
                throw;
            }
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            try
            {
                return MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao obter coleção para tipo {Type}", typeof(TEntity).Name);
                throw;
            }
        }

        public string GetDatabaseName() => MongoDatabase.DatabaseNamespace.DatabaseName;

        public bool DatabaseExists()
        {
            try
            {
                return MongoDatabase.ListCollections().ToList().Count > 0;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erro ao verificar se o banco de dados existe");
                return false;
            }
        }

        #endregion

        #region Essential Repositories for Label System

        // ETIQUETAS
        public IRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroes = null!;

        // PRODUTOS
        public IRepositoryLastUpdate<DtoProduto> _repositoryProduto = null!;

        // EMPRESAS
        public IRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresa = null!;

        // PESSOAS (Clientes/Fornecedores)
        public IRepositoryLastUpdate<DtoPessoa> _repositoryPessoa = null!;

        // TABELA DE PREÇOS
        public IRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPreco = null!;

        // DEPÓSITOS
        public IRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDeposito = null!;

        #endregion

        #region Private Repository Attributes

        public IRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroesAtributo = null!;
        public IRepositoryLastUpdate<DtoProduto> _repositoryProdutoAtributo = null!;
        public IRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresaAtributo = null!;
        public IRepositoryLastUpdate<DtoPessoa> _repositoryPessoaAtributo = null!;
        public IRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPrecoAtributo = null!;
        public IRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDepositoAtributo = null!;

        #endregion
    }
}
