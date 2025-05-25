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
        private Hashtable _repositories;
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
        }

        #endregion

        #region Mongo Repositorios Getters

        public IRepository<T> CreateRepository<T>() where T : IEntity
        {
            CheckDispose();

            var typeId = typeof(T).FullName.GetHashCode();

            if (!_repositories.ContainsKey(typeId))
            {
                var repositoryInstance = (MongoRepository<T>)Activator.CreateInstance(typeof(MongoRepository<T>), MongoDatabase);
                _repositories.Add(typeId, repositoryInstance);
            }

            return (MongoRepository<T>)_repositories[typeId];
        }

        public IRepositoryLastUpdate<T> CreateRepositoryLastUpdate<T>() where T : IEntityLastUpdate
        {
            CheckDispose();

            var typeId = typeof(T).FullName.GetHashCode();

            if (!_repositories.ContainsKey(typeId))
            {
                var repositoryInstance = (MongoRepositoryLastUpdate<T>)Activator.CreateInstance(typeof(MongoRepositoryLastUpdate<T>), MongoDatabase);
                _repositories.Add(typeId, repositoryInstance);
            }

            return (MongoRepositoryLastUpdate<T>)_repositories[typeId];
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
        public MongoRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroes
        {
            get
            {
                if (_repositoryEtiquetasPadroesAtributo == null)
                    _repositoryEtiquetasPadroesAtributo = new MongoRepository<DtoEtiquetasPadroes>(MongoDatabase);
                return _repositoryEtiquetasPadroesAtributo;
            }
        }

        // PRODUTOS
        public MongoRepositoryLastUpdate<DtoProduto> _repositoryProduto
        {
            get
            {
                if (_repositoryProdutoAtributo == null)
                    _repositoryProdutoAtributo = new MongoRepositoryLastUpdate<DtoProduto>(MongoDatabase);
                return _repositoryProdutoAtributo;
            }
        }

        // EMPRESAS
        public MongoRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresa
        {
            get
            {
                if (_repositoryEmpresaAtributo == null)
                    _repositoryEmpresaAtributo = new MongoRepositoryLastUpdate<DtoEmpresa>(MongoDatabase);
                return _repositoryEmpresaAtributo;
            }
        }

        // PESSOAS (Clientes/Fornecedores)
        public MongoRepositoryLastUpdate<DtoPessoa> _repositoryPessoa
        {
            get
            {
                if (_repositoryPessoaAtributo == null)
                    _repositoryPessoaAtributo = new MongoRepositoryLastUpdate<DtoPessoa>(MongoDatabase);
                return _repositoryPessoaAtributo;
            }
        }

        // TABELA DE PREÇOS
        public MongoRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPreco
        {
            get
            {
                if (_repositoryProdutoTabelaPrecoAtributo == null)
                    _repositoryProdutoTabelaPrecoAtributo = new MongoRepositoryLastUpdate<DtoProdutoTabela>(MongoDatabase);
                return _repositoryProdutoTabelaPrecoAtributo;
            }
        }

        // DEPÓSITOS
        public MongoRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDeposito
        {
            get
            {
                if (_repositoryDepositoAtributo == null)
                    _repositoryDepositoAtributo = new MongoRepositoryLastUpdate<DtoEstoqueDeposito>(MongoDatabase);
                return _repositoryDepositoAtributo;
            }
        }

        #endregion

        #region Private Repository Attributes

        private MongoRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroesAtributo;
        private MongoRepositoryLastUpdate<DtoProduto> _repositoryProdutoAtributo;
        private MongoRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresaAtributo;
        private MongoRepositoryLastUpdate<DtoPessoa> _repositoryPessoaAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPrecoAtributo;
        private MongoRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDepositoAtributo;

        #endregion
    }
}
