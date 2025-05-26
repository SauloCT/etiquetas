using MongoDB.Driver;
using VendaERP.Core.Models;
using System.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using App.VendaERP.Core.Models;

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
    }

    public class DBAccess : IDBAccess, IDisposable
    {
        private IMongoDatabase MongoDatabase;
        private Hashtable _repositories = null!;
        private bool _disposed;

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
                    _repositories.Clear();
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

        public DBAccess(IOptions<DBSettings> dbSettings)
        {
            ConventionPack conventionPack = new ConventionPack();
            conventionPack.Add(new IgnoreExtraElementsConvention(ignoreExtraElements: true));
            ConventionRegistry.Register("SIGE Conventions", conventionPack, (Type t) => true);

            var stringConnection = string.Format("mongodb+srv://{0}:{1}@{2}/{3}?readPreference=primaryPreferred&ssl=false&connectTimeoutMS=350000",
                dbSettings.Value.DBUser,
                dbSettings.Value.DBPassword,
                dbSettings.Value.DbHost,
                dbSettings.Value.DBName);

            var dbClient = new MongoClient(stringConnection);
            var dataBase = dbClient.GetDatabase(dbSettings.Value.DBName);
            MongoDatabase = dataBase;
            CreateHash();
            
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
        }

        #endregion

        #region Mongo Repositorios Getters

        public IRepository<T> CreateRepository<T>() where T : IEntity
        {
            CheckDispose();

            var typeId = typeof(T).FullName?.GetHashCode() ?? 0;

            if (!_repositories.ContainsKey(typeId))
            {
                var repositoryInstance = (MongoRepository<T>?)Activator.CreateInstance(typeof(MongoRepository<T>), MongoDatabase);
                if (repositoryInstance != null)
                {
                    _repositories.Add(typeId, repositoryInstance);
                }
            }

            return (MongoRepository<T>)(_repositories[typeId] ?? throw new InvalidOperationException("Repository not found"));
        }

        public IRepositoryLastUpdate<T> CreateRepositoryLastUpdate<T>() where T : IEntityLastUpdate
        {
            CheckDispose();

            var typeId = typeof(T).FullName?.GetHashCode() ?? 0;

            if (!_repositories.ContainsKey(typeId))
            {
                var repositoryInstance = (MongoRepositoryLastUpdate<T>?)Activator.CreateInstance(typeof(MongoRepositoryLastUpdate<T>), MongoDatabase);
                if (repositoryInstance != null)
                {
                    _repositories.Add(typeId, repositoryInstance);
                }
            }

            return (MongoRepositoryLastUpdate<T>)(_repositories[typeId] ?? throw new InvalidOperationException("Repository not found"));
        }

        public MongoRepository<T> GetRepositoryFromType<T>() where T : IEntity
        {
            return new MongoRepository<T>(MongoDatabase);
        }

        public MongoRepositoryLastUpdate<T> GetRepositoryLastUpdateFromType<T>() where T : IEntityLastUpdate
        {
            return new MongoRepositoryLastUpdate<T>(MongoDatabase);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name) =>
            MongoDatabase.GetCollection<TEntity>(name);

        public IMongoCollection<TEntity> GetCollection<TEntity>() =>
            MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);

        public string GetDatabaseName() => MongoDatabase.DatabaseNamespace.DatabaseName;

        public bool DatabaseExists()
        {
            return MongoDatabase.ListCollections().ToList().Count > 0;
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
