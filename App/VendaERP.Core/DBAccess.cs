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

        //ATRIBUTOS
        public MongoRepository<DtoAtributos> _repositoryAtributos
        {
            get
            {
                if (_repositoryAtributosAtributo == null)
                    _repositoryAtributosAtributo = new MongoRepository<DtoAtributos>(MongoDatabase);

                return _repositoryAtributosAtributo;
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
        public MongoRepositoryLastUpdate<DtoProdutoFaturamentoParcial> _repositoryProdutoFaturamentoParcial
        {
            get
            {
                if (_repositoryProdutoFaturamentoParcialAtributo == null)
                    _repositoryProdutoFaturamentoParcialAtributo = new MongoRepositoryLastUpdate<DtoProdutoFaturamentoParcial>(MongoDatabase);

                return _repositoryProdutoFaturamentoParcialAtributo;
            }
        }
        public MongoRepository<DtoProdutoSimilar> _repositoryProdutoSimilar
        {
            get
            {
                if (_repositoryProdutoSimilarAtributo == null)
                    _repositoryProdutoSimilarAtributo = new MongoRepository<DtoProdutoSimilar>(MongoDatabase);

                return _repositoryProdutoSimilarAtributo;
            }
        }
        public MongoRepository<DtoProdutoComposicao> _repositoryProdutoComposto
        {
            get
            {
                if (_repositoryProdutoCompostoAtributo == null)
                    _repositoryProdutoCompostoAtributo = new MongoRepository<DtoProdutoComposicao>(MongoDatabase);

                return _repositoryProdutoCompostoAtributo;
            }
        }
    
        public MongoRepositoryLastUpdate<DtoImagemProduto> _repositoryProdutoImagem
        {
            get
            {
                if (_repositoryProdutoImagemAtributo == null)
                    _repositoryProdutoImagemAtributo = new MongoRepositoryLastUpdate<DtoImagemProduto>(MongoDatabase);

                return _repositoryProdutoImagemAtributo;
            }
        }
        public MongoRepository<DtoCategoriaProduto> _repositoryProdutoCategoria
        {
            get
            {
                if (_repositoryProdutoCategoriaAtributo == null)
                    _repositoryProdutoCategoriaAtributo = new MongoRepository<DtoCategoriaProduto>(MongoDatabase);

                return _repositoryProdutoCategoriaAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPreco
        {
            get
            {
                if (_repositoryProdutoTabelaPrecoAtributo == null)
                    _repositoryProdutoTabelaPrecoAtributo = new MongoRepositoryLastUpdate<DtoProdutoTabela>(MongoDatabase);

                return _repositoryProdutoTabelaPrecoAtributo;
            }
        }
        public MongoRepository<DtoTamanho> _repositoryProdutoTamanho
        {
            get
            {
                if (_repositoryProdutoTamanhoAtributo == null)
                    _repositoryProdutoTamanhoAtributo = new MongoRepository<DtoTamanho>(MongoDatabase);

                return _repositoryProdutoTamanhoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoMarca> _repositoryProdutoMarca
        {
            get
            {
                if (_repositoryProdutoMarcas == null)
                    _repositoryProdutoMarcas = new MongoRepositoryLastUpdate<DtoMarca>(MongoDatabase);

                return _repositoryProdutoMarcas;
            }
        }
        public MongoRepositoryLastUpdate<DtoProdutoPreco> _repositoryProdutoPreco
        {
            get
            {
                if (_repositoryProdutoPrecoAtributo == null)
                    _repositoryProdutoPrecoAtributo = new MongoRepositoryLastUpdate<DtoProdutoPreco>(MongoDatabase);

                return _repositoryProdutoPrecoAtributo;
            }
        }
        public MongoRepository<DtoProdutoAtributo> _repositoryProdutoAttributo
        {
            get
            {
                if (_repositoryProdutoAttributoAtributo == null)
                    _repositoryProdutoAttributoAtributo = new MongoRepository<DtoProdutoAtributo>(MongoDatabase);

                return _repositoryProdutoAttributoAtributo;
            }
        }
        public MongoRepository<DtoProdutoGrade> _repositoryProdutoGrade
        {
            get
            {
                if (_repositoryProdutoGradeAtributo == null)
                    _repositoryProdutoGradeAtributo = new MongoRepository<DtoProdutoGrade>(MongoDatabase);

                return _repositoryProdutoGradeAtributo;
            }
        }
        public MongoRepository<DtoProdutoCatalogo> _repositoryProdutoCatalogo
        {
            get
            {
                if (_repositoryProdutoCatalogoAtributo == null)
                    _repositoryProdutoCatalogoAtributo = new MongoRepository<DtoProdutoCatalogo>(MongoDatabase);

                return _repositoryProdutoCatalogoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoProdutoCaracteristica> _repositoryProdutoCaracteristica
        {
            get
            {
                if (_repositoryProdutoCaracteristicaAtributo == null)
                    _repositoryProdutoCaracteristicaAtributo = new MongoRepositoryLastUpdate<DtoProdutoCaracteristica>(MongoDatabase);

                return _repositoryProdutoCaracteristicaAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoProdutoOpcoesAlimenticio> _repositoryProdutoOpcoesAlimenticio
        {
            get
            {
                if (_repositoryProdutoOpcoesAlimenticioAtributo == null)
                    _repositoryProdutoOpcoesAlimenticioAtributo = new MongoRepositoryLastUpdate<DtoProdutoOpcoesAlimenticio>(MongoDatabase);

                return _repositoryProdutoOpcoesAlimenticioAtributo;
            }
        }

        

        // ESTOQUE
        public MongoRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDeposito
        {
            get
            {
                if (_repositoryDepositoAtributo == null)
                    _repositoryDepositoAtributo = new MongoRepositoryLastUpdate<DtoEstoqueDeposito>(MongoDatabase);

                return _repositoryDepositoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoEstoqueDepositoProduto> _repositoryDepositoProduto
        {
            get
            {
                if (_repositoryDepositoProdutoAtributo == null)
                    _repositoryDepositoProdutoAtributo = new MongoRepositoryLastUpdate<DtoEstoqueDepositoProduto>(MongoDatabase);

                return _repositoryDepositoProdutoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoEstoqueDepositoProdutoLote> _repositoryDepositoProdutoLote
        {
            get
            {
                if (_repositoryDepositoProdutoLoteAtributo == null)
                    _repositoryDepositoProdutoLoteAtributo = new MongoRepositoryLastUpdate<DtoEstoqueDepositoProdutoLote>(MongoDatabase);

                return _repositoryDepositoProdutoLoteAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoEstoqueSaida> _repositoryEstoqueSaida
        {
            get
            {
                if (_repositoryEstoqueSaidaAtributo == null)
                    _repositoryEstoqueSaidaAtributo = new MongoRepositoryLastUpdate<DtoEstoqueSaida>(MongoDatabase);

                return _repositoryEstoqueSaidaAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoEstoqueEntrada> _repositoryEstoqueEntrada
        {
            get
            {
                if (_repositoryEstoqueEntradaAtributo == null)
                    _repositoryEstoqueEntradaAtributo = new MongoRepositoryLastUpdate<DtoEstoqueEntrada>(MongoDatabase);

                return _repositoryEstoqueEntradaAtributo;
            }
        }
        public MongoRepository<DtoEstoqueUnidade> _repositoryEstoqueUnidade
        {
            get
            {
                if (_repositoryEstoqueUnidadeAtributo == null)
                    _repositoryEstoqueUnidadeAtributo = new MongoRepository<DtoEstoqueUnidade>(MongoDatabase);

                return _repositoryEstoqueUnidadeAtributo;
            }
        }

        public MongoRepositoryLastUpdate<DtoEstoqueProdutoStatus> _repositoryEstoqueProdutoStatus
        {
            get
            {
                if (_repositoryEstoqueProdutoStatusAtributo == null)
                    _repositoryEstoqueProdutoStatusAtributo = new MongoRepositoryLastUpdate<DtoEstoqueProdutoStatus>(MongoDatabase);

                return _repositoryEstoqueProdutoStatusAtributo;
            }
        }


        public MongoRepository<DtoEstoqueReserva> _repositoryEstoqueReserva
        {
            get
            {
                if (_repositoryEstoqueReservaAtributo == null)
                    _repositoryEstoqueReservaAtributo = new MongoRepository<DtoEstoqueReserva>(MongoDatabase);

                return _repositoryEstoqueReservaAtributo;
            }
        }

        public MongoRepository<DtoEstoqueReservaProduto> _repositoryEstoqueReservaProduto
        {
            get
            {
                if (_repositoryEstoqueReservaProdutoAtributo == null)
                    _repositoryEstoqueReservaProdutoAtributo = new MongoRepository<DtoEstoqueReservaProduto>(MongoDatabase);

                return _repositoryEstoqueReservaProdutoAtributo;
            }
        }
        public MongoRepository<DtoEstoqueMovEntreDepositos> _repositoryEstoqueMovEntreDeposito
        {
            get
            {
                if (_repositoryEstoqueMovEntreDepositosAtributo == null)
                    _repositoryEstoqueMovEntreDepositosAtributo = new MongoRepository<DtoEstoqueMovEntreDepositos>(MongoDatabase);

                return _repositoryEstoqueMovEntreDepositosAtributo;
            }
        }
        public MongoRepository<DtoEstoqueMovEntreDepositosProduto> _repositoryEstoqueMovEntreDepositoProduto
        {
            get
            {
                if (_repositoryEstoqueMovEntreDepositosProdutoAtributo == null)
                    _repositoryEstoqueMovEntreDepositosProdutoAtributo = new MongoRepository<DtoEstoqueMovEntreDepositosProduto>(MongoDatabase);

                return _repositoryEstoqueMovEntreDepositosProdutoAtributo;
            }
        }

        public MongoRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroes
        {
            get
            {
                if (_repositoryEtiquetasPadroesAtributo == null)
                    _repositoryEtiquetasPadroesAtributo = new MongoRepository<DtoEtiquetasPadroes>(MongoDatabase);

                return _repositoryEtiquetasPadroesAtributo;
            }
        }


        // COMPRA
        public MongoRepository<DtoOrdemCompraitem> _repositoryOrdemCompraItem
        {
            get
            {
                if (_repositoryOrdemCompraItemAtributo == null)
                    _repositoryOrdemCompraItemAtributo = new MongoRepository<DtoOrdemCompraitem>(MongoDatabase);

                return _repositoryOrdemCompraItemAtributo;
            }
        }
       
        public MongoRepository<DtoOrdemCompra> _repositoryOrdemCompra
        {
            get
            {
                if (_repositoryOrdemCompraAtributo == null)
                    _repositoryOrdemCompraAtributo = new MongoRepository<DtoOrdemCompra>(MongoDatabase);

                return _repositoryOrdemCompraAtributo;
            }
        }

        // ENTRADA
        public MongoRepository<DtoNotaEntradaProduto> _repositoryNotaEntradaProduto
        {
            get
            {
                if (_repositoryNotaEntradaProdutoAtributo == null)
                    _repositoryNotaEntradaProdutoAtributo = new MongoRepository<DtoNotaEntradaProduto>(MongoDatabase);

                return _repositoryNotaEntradaProdutoAtributo;
            }
        }
        public MongoRepository<DtoNotaEntrada> _repositoryNotaEntrada
        {
            get
            {
                if (_repositoryNotaEntradaAtributo == null)
                    _repositoryNotaEntradaAtributo = new MongoRepository<DtoNotaEntrada>(MongoDatabase);

                return _repositoryNotaEntradaAtributo;
            }
        }
        public MongoRepository<DtoNotaEntradaDuplicata> _repositoryNotaEntradaDuplicata
        {
            get
            {
                if (_repositoryNotaEntradaDuplicataAtributo == null)
                    _repositoryNotaEntradaDuplicataAtributo = new MongoRepository<DtoNotaEntradaDuplicata>(MongoDatabase);

                return _repositoryNotaEntradaDuplicataAtributo;
            }
        }


        // VENDA
        public MongoRepositoryLastUpdate<DtoVendaProduto> _repositoryVendaProduto
        {
            get
            {
                if (_repositoryVendaProdutoAtributo == null)
                    _repositoryVendaProdutoAtributo = new MongoRepositoryLastUpdate<DtoVendaProduto>(MongoDatabase);

                return _repositoryVendaProdutoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoVenda> _repositoryVenda
        {
            get
            {
                if (_repositoryVendaAtributo == null)
                    _repositoryVendaAtributo = new MongoRepositoryLastUpdate<DtoVenda>(MongoDatabase);

                return _repositoryVendaAtributo;
            }
        }
        public MongoRepository<DtoVendaEquipamento> _repositoryVendaEquipamento
        {
            get
            {
                if (_repositoryVendaEquipamentoAtributo == null)
                    _repositoryVendaEquipamentoAtributo = new MongoRepository<DtoVendaEquipamento>(MongoDatabase);

                return _repositoryVendaEquipamentoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoVendaAgrupamento> _repositoryVendaAgrupamento
        {
            get
            {
                if (_repositoryVendaAgrupamentoAtributo == null)
                    _repositoryVendaAgrupamentoAtributo = new MongoRepositoryLastUpdate<DtoVendaAgrupamento>(MongoDatabase);

                return _repositoryVendaAgrupamentoAtributo;
            }
        }

        public MongoRepository<DtoNFesMultiVendas> _repositoryInitNFesMultiVendas
        {
            get
            {
                if (_repositoryInitNFesMultiVendasAtributo == null)
                    _repositoryInitNFesMultiVendasAtributo = new MongoRepository<DtoNFesMultiVendas>(MongoDatabase);

                return _repositoryInitNFesMultiVendasAtributo;
            }
        }

        public MongoRepository<DtoVendaVeiculo> _repositoryVendaVeiculo
        {
            get
            {
                if (_repositoryVendaVeiculoAtributo == null)
                    _repositoryVendaVeiculoAtributo = new MongoRepository<DtoVendaVeiculo>(MongoDatabase);

                return _repositoryVendaVeiculoAtributo;
            }
        }

        public MongoRepository<DtoVendaLaudo> _repositoryVendaLaudo
        {
            get
            {
                if (_repositoryVendaLaudoAtributo == null)
                    _repositoryVendaLaudoAtributo = new MongoRepository<DtoVendaLaudo>(MongoDatabase);

                return _repositoryVendaLaudoAtributo;
            }
        }
        public MongoRepository<DtoVendaRapida> _repositoryVendaRapida
        {
            get
            {
                if (_repositoryVendaRapidaAtributo == null)
                    _repositoryVendaRapidaAtributo = new MongoRepository<DtoVendaRapida>(MongoDatabase);

                return _repositoryVendaRapidaAtributo;
            }
        }
        public MongoRepository<DtoVendaStatus> _repositoryVendaStatus
        {
            get
            {
                if (_repositoryVendaStatusAtributo == null)
                    _repositoryVendaStatusAtributo = new MongoRepository<DtoVendaStatus>(MongoDatabase);

                return _repositoryVendaStatusAtributo;
            }
        }
        public MongoRepository<DtoVendaTerceiros> _repositoryVendaTerceiros
        {
            get
            {
                if (_repositoryVendaTerceirosAtributo == null)
                    _repositoryVendaTerceirosAtributo = new MongoRepository<DtoVendaTerceiros>(MongoDatabase);

                return _repositoryVendaTerceirosAtributo;
            }
        }

        public MongoRepositoryLastUpdate<DtoVendaCategoria> _repositoryVendaCategoria
        {
            get
            {
                if (_repositoryVendaCategoriaAtributo == null)
                    _repositoryVendaCategoriaAtributo = new MongoRepositoryLastUpdate<DtoVendaCategoria>(MongoDatabase);

                return _repositoryVendaCategoriaAtributo;
            }
        }

        public MongoRepository<DtoVendaMeta> _repositoryVendaMeta
        {
            get
            {
                if (_repositoryVendaMetaAtributo == null)
                    _repositoryVendaMetaAtributo = new MongoRepository<DtoVendaMeta>(MongoDatabase);

                return _repositoryVendaMetaAtributo;
            }
        }

    

        //EXPEDICAO
        public MongoRepository<DtoOrdemExpedicao> _repositoryOrdemExpedicao
        {
            get
            {
                if (_repositoryOrdemExpedicaoAtributo == null)
                    _repositoryOrdemExpedicaoAtributo = new MongoRepository<DtoOrdemExpedicao>(MongoDatabase);

                return _repositoryOrdemExpedicaoAtributo;
            }
        }
        public MongoRepository<DtoOrdemExpedicaoItem> _repositoryOrdemExpedicaoItem
        {
            get
            {
                if (_repositoryOrdemExpedicaoItemAtributo == null)
                    _repositoryOrdemExpedicaoItemAtributo = new MongoRepository<DtoOrdemExpedicaoItem>(MongoDatabase);

                return _repositoryOrdemExpedicaoItemAtributo;
            }
        }

        //PDV
        

        public MongoRepositoryLastUpdate<DtoVale> _repositoryVale
        {
            get
            {
                if (_repositoryValeAtributo == null)
                    _repositoryValeAtributo = new MongoRepositoryLastUpdate<DtoVale>(MongoDatabase);

                return _repositoryValeAtributo;
            }
        }

        //FINANCEIRO
        public MongoRepositoryLastUpdate<DtoBanco> _repositoryBanco
        {
            get
            {
                if (_repositoryBancoAtributo == null)
                    _repositoryBancoAtributo = new MongoRepositoryLastUpdate<DtoBanco>(MongoDatabase);

                return _repositoryBancoAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoLancamento> _repositoryLancamento
        {
            get
            {
                if (_repositoryLancamentoAtributo == null)
                    _repositoryLancamentoAtributo = new MongoRepositoryLastUpdate<DtoLancamento>(MongoDatabase);

                return _repositoryLancamentoAtributo;
            }
        }

        public MongoRepositoryLastUpdate<DtoCheque> _repositoryCheque
        {
            get
            {
                if (_repositoryChequeAtributo == null)
                    _repositoryChequeAtributo = new MongoRepositoryLastUpdate<DtoCheque>(MongoDatabase);

                return _repositoryChequeAtributo;
            }
        }

        private MongoRepository<DtoSaldoBancarioMensal> _saldosBancariosMensal;
        public MongoRepository<DtoSaldoBancarioMensal> SaldosBancariosMensal
        {
            get
            {
                if (_saldosBancariosMensal == null)
                    _saldosBancariosMensal = new MongoRepository<DtoSaldoBancarioMensal>(MongoDatabase);

                return _saldosBancariosMensal;
            }
        }

        public MongoRepositoryLastUpdate<DtoPlanoDeConta> _repositoryPlanoDeConta
        {
            get
            {
                if (_repositoryPlanoDeContaAtributo == null)
                    _repositoryPlanoDeContaAtributo = new MongoRepositoryLastUpdate<DtoPlanoDeConta>(MongoDatabase);

                return _repositoryPlanoDeContaAtributo;
            }
        }
        public MongoRepository<DtoGrupoDRE> _repositoryGrupoDRE
        {
            get
            {
                if (_repositoryGrupoDREAtributo == null)
                    _repositoryGrupoDREAtributo = new MongoRepository<DtoGrupoDRE>(MongoDatabase);

                return _repositoryGrupoDREAtributo;
            }
        }
        public MongoRepositoryLastUpdate<DtoFormaPagamento> _repositoryFormaPagamento
        {
            get
            {
                if (_repositoryFormaPagamentoAtributo == null)
                    _repositoryFormaPagamentoAtributo = new MongoRepositoryLastUpdate<DtoFormaPagamento>(MongoDatabase);

                return _repositoryFormaPagamentoAtributo;
            }
        }
        public MongoRepository<DtoCentroCustos> _repositoryCentroCusto
        {
            get
            {
                if (_repositoryCentroCustosAtributo == null)
                    _repositoryCentroCustosAtributo = new MongoRepository<DtoCentroCustos>(MongoDatabase);

                return _repositoryCentroCustosAtributo;
            }
        }

        private MongoRepositoryLastUpdate<DtoBoleto> _repositoryBoletoAtributo;
        public MongoRepositoryLastUpdate<DtoBoleto> _repositoryBoleto
        {
            get
            {
                if (_repositoryBoletoAtributo == null)
                    _repositoryBoletoAtributo = new MongoRepositoryLastUpdate<DtoBoleto>(MongoDatabase);

                return _repositoryBoletoAtributo;
            }
        }


        private MongoRepository<DtoBoletoRemessa> _repositoryBoletoRemessaAtributo;
        public MongoRepository<DtoBoletoRemessa> _repositoryBoletoRemessa
        {
            get
            {
                if (_repositoryBoletoRemessaAtributo == null)
                    _repositoryBoletoRemessaAtributo = new MongoRepository<DtoBoletoRemessa>(MongoDatabase);

                return _repositoryBoletoRemessaAtributo;
            }
        }

        public MongoRepository<DtoLancamentoGrupo> _repositoryLancamentoGrupo
        {
            get
            {
                if (_repositoryLancamentoGrupoAtributo == null)
                    _repositoryLancamentoGrupoAtributo = new MongoRepository<DtoLancamentoGrupo>(MongoDatabase);

                return _repositoryLancamentoGrupoAtributo;
            }
        }
        public MongoRepository<DtoTransferenciaBancaria> _repositoryTransferenciaBancaria
        {
            get
            {
                if (_repositoryTransferenciaBancariaAtributo == null)
                    _repositoryTransferenciaBancariaAtributo = new MongoRepository<DtoTransferenciaBancaria>(MongoDatabase);

                return _repositoryTransferenciaBancariaAtributo;
            }
        }

        //PESSOAS
        public MongoRepositoryLastUpdate<DtoPessoa> _repositoryPessoa
        {
            get
            {
                if (_repositoryPessoaAtributo == null)
                    _repositoryPessoaAtributo = new MongoRepositoryLastUpdate<DtoPessoa>(MongoDatabase);

                return _repositoryPessoaAtributo;
            }
        }
        public MongoRepository<DtoPessoaContato> _repositoryPessoaContato
        {
            get
            {
                if (_repositoryPessoaContatoAtributo == null)
                    _repositoryPessoaContatoAtributo = new MongoRepository<DtoPessoaContato>(MongoDatabase);

                return _repositoryPessoaContatoAtributo;
            }
        }
        public MongoRepository<DtoPessoaGrupo> _repositoryPessoaGrupo
        {
            get
            {
                if (_repositoryPessoaGrupoAtributo == null)
                    _repositoryPessoaGrupoAtributo = new MongoRepository<DtoPessoaGrupo>(MongoDatabase);

                return _repositoryPessoaGrupoAtributo;
            }
        }
        public MongoRepository<DtoProcesso> _repositoryProcesso
        {
            get
            {
                if (_repositoryProcessoAtributo == null)
                    _repositoryProcessoAtributo = new MongoRepository<DtoProcesso>(MongoDatabase);

                return _repositoryProcessoAtributo;
            }
        }

        //Empresas
        public MongoRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresa
        {
            get
            {
                if (_repositoryEmpresaAtributo == null)
                    _repositoryEmpresaAtributo = new MongoRepositoryLastUpdate<DtoEmpresa>(MongoDatabase);

                return _repositoryEmpresaAtributo;
            }
        }

        //MODULO MRP
        public MongoRepository<DtoEtapa> _repositoryEtapa
        {
            get
            {
                if (_repositoryEtapaAtributo == null)
                    _repositoryEtapaAtributo = new MongoRepository<DtoEtapa>(MongoDatabase);

                return _repositoryEtapaAtributo;
            }
        }
        public MongoRepository<DtoProcessoProdutivo> _repositoryProcessoProdutivo
        {
            get
            {
                if (_repositoryProcessoProdutivoAtributo == null)
                    _repositoryProcessoProdutivoAtributo = new MongoRepository<DtoProcessoProdutivo>(MongoDatabase);

                return _repositoryProcessoProdutivoAtributo;
            }
        }
        public MongoRepository<DtoProdutoQualidade> _repositoryProdutoQualidade
        {
            get
            {
                if (_repositoryProdutoQualidadeAtributo == null)
                    _repositoryProdutoQualidadeAtributo = new MongoRepository<DtoProdutoQualidade>(MongoDatabase);

                return _repositoryProdutoQualidadeAtributo;
            }
        }
        public MongoRepository<DtoOrdemProducao> _repositoryOrdemProducao
        {
            get
            {
                if (_repositoryOrdemProducaoAtributo == null)
                    _repositoryOrdemProducaoAtributo = new MongoRepository<DtoOrdemProducao>(MongoDatabase);

                return _repositoryOrdemProducaoAtributo;
            }
        }
        public MongoRepository<DtoOrdemProducaoHistorico> _repositoryOrdemProducaoHistorico
        {
            get
            {
                if (_repositoryOrdemProducaoHistoricoAtributo == null)
                    _repositoryOrdemProducaoHistoricoAtributo = new MongoRepository<DtoOrdemProducaoHistorico>(MongoDatabase);

                return _repositoryOrdemProducaoHistoricoAtributo;
            }
        }
        public MongoRepository<DtoOrdemProducaoComposicao> _repositoryOrdemProducaoComposicao
        {
            get
            {
                if (_repositoryOrdemProducaoComposicaoAtributo == null)
                    _repositoryOrdemProducaoComposicaoAtributo = new MongoRepository<DtoOrdemProducaoComposicao>(MongoDatabase);

                return _repositoryOrdemProducaoComposicaoAtributo;
            }
        }
        public MongoRepository<DtoOrdemProducaoStatus> _repositoryOrdemProducaoStatus
        {
            get
            {
                if (_repositoryOrdemProducaoStatusAtributo == null)
                    _repositoryOrdemProducaoStatusAtributo = new MongoRepository<DtoOrdemProducaoStatus>(MongoDatabase);

                return _repositoryOrdemProducaoStatusAtributo;
            }
        }
        public MongoRepository<DtoOrdemProducaoCheckList> _repositoryOrdemProducaoCheckList
        {
            get
            {
                if (_repositoryOrdemProducaoCheckListAtributo == null)
                    _repositoryOrdemProducaoCheckListAtributo = new MongoRepository<DtoOrdemProducaoCheckList>(MongoDatabase);

                return _repositoryOrdemProducaoCheckListAtributo;
            }
        }
        public MongoRepository<DtoLinhaProducao> _repositoryLinhaProducao
        {
            get
            {
                if (_repositoryLinhaProducaoAtributo == null)
                    _repositoryLinhaProducaoAtributo = new MongoRepository<DtoLinhaProducao>(MongoDatabase);

                return _repositoryLinhaProducaoAtributo;
            }
        }
        

        

        public MongoRepository<DtoLogNFe> _repositoryLogNFe
        {
            get
            {
                if (_repositoryLogNFEAtributo == null)
                    _repositoryLogNFEAtributo = new MongoRepository<DtoLogNFe>(MongoDatabase);

                return _repositoryLogNFEAtributo;
            }
        }

        public MongoRepository<DtoLogNFSe> _repositoryLogNFSe
        {
            get
            {
                if (_repositoryLogNFSEAtributo == null)
                    _repositoryLogNFSEAtributo = new MongoRepository<DtoLogNFSe>(MongoDatabase);

                return _repositoryLogNFSEAtributo;
            }
        }

        public MongoRepository<DtoLogMDFe> _repositoryLogMDFe
        {
            get
            {
                if (_repositoryLogMDFEAtributo == null)
                    _repositoryLogMDFEAtributo = new MongoRepository<DtoLogMDFe>(MongoDatabase);

                return _repositoryLogMDFEAtributo;
            }
        }

        

        // SERVIÇOS
        
        public MongoRepository<DtoOrdemServico> _repositoryOrdemServico
        {
            get
            {
                if (_repositoryOrdemServicoAtributo == null)
                    _repositoryOrdemServicoAtributo = new MongoRepository<DtoOrdemServico>(MongoDatabase);

                return _repositoryOrdemServicoAtributo;
            }
        }


        // CRM
        public MongoRepository<DtoOportunidade> _repositoryOportunidade
        {
            get
            {
                if (_repositoryOportunidadeAtributo == null)
                    _repositoryOportunidadeAtributo = new MongoRepository<DtoOportunidade>(MongoDatabase);

                return _repositoryOportunidadeAtributo;
            }
        }

        public MongoRepository<DtoOportunidadeEvento> _repositoryOportunidadeEvento
        {
            get
            {
                if (_repositoryOportunidadeEventoAtributo == null)
                    _repositoryOportunidadeEventoAtributo = new MongoRepository<DtoOportunidadeEvento>(MongoDatabase);

                return _repositoryOportunidadeEventoAtributo;
            }
        }

        //AGENDA
        public MongoRepository<DtoAgenda> _repositoryAgenda
        {
            get
            {
                if (_repositoryAgendaAtributo == null)
                    _repositoryAgendaAtributo = new MongoRepository<DtoAgenda>(MongoDatabase);

                return _repositoryAgendaAtributo;
            }
        }

        

        


        //CONTRATO
        public MongoRepository<DtoContrato> _repositoryContrato
        {
            get
            {
                if (_repositoryContratoAtributo == null)
                    _repositoryContratoAtributo = new MongoRepository<DtoContrato>(MongoDatabase);

                return _repositoryContratoAtributo;
            }
        }
        public MongoRepository<DtoTipoContrato> _repositoryContratoTipo
        {
            get
            {
                if (_repositoryContratoTipoAtributo == null)
                    _repositoryContratoTipoAtributo = new MongoRepository<DtoTipoContrato>(MongoDatabase);

                return _repositoryContratoTipoAtributo;
            }
        }

        


        //PROJETO
        public MongoRepository<DtoProjeto> _repositoryProjeto
        {
            get
            {
                if (_repositoryProjetoAtributo == null)
                    _repositoryProjetoAtributo = new MongoRepository<DtoProjeto>(MongoDatabase);

                return _repositoryProjetoAtributo;
            }
        }

        //COTAÇÃO
        public MongoRepository<DtoCotacao> _repositoryCotacao
        {
            get
            {
                if (_repositoryCotacaoAtributo == null)
                    _repositoryCotacaoAtributo = new MongoRepository<DtoCotacao>(MongoDatabase);

                return _repositoryCotacaoAtributo;
            }
        }
        public MongoRepository<DtoCotacaoItem> _repositoryCotacaoItem
        {
            get
            {
                if (_repositoryCotacaoItemAtributo == null)
                    _repositoryCotacaoItemAtributo = new MongoRepository<DtoCotacaoItem>(MongoDatabase);

                return _repositoryCotacaoItemAtributo;
            }
        }
        public MongoRepository<DtoCotacaoCotista> _repositoryCotacaoCotista
        {
            get
            {
                if (_repositoryCotacaoCotistaAtributo == null)
                    _repositoryCotacaoCotistaAtributo = new MongoRepository<DtoCotacaoCotista>(MongoDatabase);

                return _repositoryCotacaoCotistaAtributo;
            }
        }
        public MongoRepository<DtoCotacaoItemCotado> _repositoryCotacaoItemCotado
        {
            get
            {
                if (_repositoryCotacaoItemCotadoAtributo == null)
                    _repositoryCotacaoItemCotadoAtributo = new MongoRepository<DtoCotacaoItemCotado>(MongoDatabase);

                return _repositoryCotacaoItemCotadoAtributo;
            }
        }
        

        //Log
        public MongoRepository<Log> _repositoryLog
        {
            get
            {
                if (_repositoryLogAtributo == null)
                    _repositoryLogAtributo = new MongoRepository<Log>(MongoDatabase);

                return _repositoryLogAtributo;
            }
        }

        
        //OFX
        private MongoRepository<DtoLogOFX> _repositoryLogOFXAtributo;
        public MongoRepository<DtoLogOFX> _repositoryLogOFX
        {
            get
            {
                if (_repositoryLogOFXAtributo == null)
                    _repositoryLogOFXAtributo = new MongoRepository<DtoLogOFX>(MongoDatabase);

                return _repositoryLogOFXAtributo;
            }
        }

        //RETORNO-Boletos
        private MongoRepository<DtoLogRetornoRemessa> _repositoryLogRetornoRemessaAtributo;
        public MongoRepository<DtoLogRetornoRemessa> _repositoryLogRetornoRemessa
        {
            get
            {
                if (_repositoryLogRetornoRemessaAtributo == null)
                    _repositoryLogRetornoRemessaAtributo = new MongoRepository<DtoLogRetornoRemessa>(MongoDatabase);

                return _repositoryLogRetornoRemessaAtributo;
            }
        }
       

        

        //Equipamentos

        public MongoRepositoryLastUpdate<DtoEquipamento> _repositoryEquipamentos
        {
            get
            {
                if (_repositoryEquipamentosAtributo == null)
                    _repositoryEquipamentosAtributo = new MongoRepositoryLastUpdate<DtoEquipamento>(MongoDatabase);

                return _repositoryEquipamentosAtributo;
            }
        }

        //Logs de alteração de status OS
        public MongoRepositoryLastUpdate<DtoLogsOrdemServico> _repositoryLogOrdemServico
        {
            get
            {
                if (_repositoryLogOrdemServicoAtributo == null)
                    _repositoryLogOrdemServicoAtributo = new MongoRepositoryLastUpdate<DtoLogsOrdemServico>(MongoDatabase);

                return _repositoryLogOrdemServicoAtributo;
            }
        }

        public MongoRepositoryLastUpdate<DtoRetornosBancarios> _repositoryRetornosBancarios
        {
            get
            {
                if (_repositoryRetornosBancariosAtributo == null)
                    _repositoryRetornosBancariosAtributo = new MongoRepositoryLastUpdate<DtoRetornosBancarios>(MongoDatabase);

                return _repositoryRetornosBancariosAtributo;
            }
        }

        // INTEGRAÇÕES
        public MongoRepository<DtoIntegracoes> _repositoryIntegracoes
        {
            get
            {
                if (_repositoryIntegracoesAtributo == null)
                    _repositoryIntegracoesAtributo = new MongoRepository<DtoIntegracoes>(MongoDatabase);

                return _repositoryIntegracoesAtributo;
            }
        }

        public MongoRepository<DtoIntegracoesTempData> _repositoryIntegracoesTempData
        {
            get
            {
                if (_repositoryIntegracoesTempDataAtributo == null)
                    _repositoryIntegracoesTempDataAtributo = new MongoRepository<DtoIntegracoesTempData>(MongoDatabase);

                return _repositoryIntegracoesTempDataAtributo;
            }
        }

        public MongoRepository<DtoIFoodAuthentication> _repositoryIFoodAuthentication
        {
            get
            {
                if (_repositoryIFoodAuthenticationAtributo == null)
                    _repositoryIFoodAuthenticationAtributo = new MongoRepository<DtoIFoodAuthentication>(MongoDatabase);

                return _repositoryIFoodAuthenticationAtributo;
            }
        }

        public MongoRepository<DtoIFoodVenda> _repositoryIFoodVenda
        {
            get
            {
                if (_repositoryIFoodVendaAtributo == null)
                    _repositoryIFoodVendaAtributo = new MongoRepository<DtoIFoodVenda>(MongoDatabase);

                return _repositoryIFoodVendaAtributo;
            }
        }

        #region Mongo Repositorios Atributos

        // PRODUTOS
        private MongoRepository<DtoAtributos> _repositoryAtributosAtributo;

        // PRODUTOS
        private MongoRepositoryLastUpdate<DtoProduto> _repositoryProdutoAtributo;
        private MongoRepository<DtoProdutoSimilar> _repositoryProdutoSimilarAtributo;
        private MongoRepository<DtoProdutoComposicao> _repositoryProdutoCompostoAtributo;
        
        private MongoRepositoryLastUpdate<DtoImagemProduto> _repositoryProdutoImagemAtributo;
        private MongoRepository<DtoCategoriaProduto> _repositoryProdutoCategoriaAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoTabela> _repositoryProdutoTabelaPrecoAtributo;
        private MongoRepositoryLastUpdate<DtoMarca> _repositoryProdutoMarcas;
        private MongoRepository<DtoTamanho> _repositoryProdutoTamanhoAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoPreco> _repositoryProdutoPrecoAtributo;
        private MongoRepository<DtoProdutoAtributo> _repositoryProdutoAttributoAtributo;
        private MongoRepository<DtoProdutoGrade> _repositoryProdutoGradeAtributo;
        private MongoRepository<DtoProdutoCatalogo> _repositoryProdutoCatalogoAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoCaracteristica> _repositoryProdutoCaracteristicaAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoOpcoesAlimenticio> _repositoryProdutoOpcoesAlimenticioAtributo;
        private MongoRepositoryLastUpdate<DtoProdutoFaturamentoParcial> _repositoryProdutoFaturamentoParcialAtributo;
        

        // ESTOQUE 
        private MongoRepositoryLastUpdate<DtoEstoqueDeposito> _repositoryDepositoAtributo;
        private MongoRepositoryLastUpdate<DtoEstoqueDepositoProduto> _repositoryDepositoProdutoAtributo;
        private MongoRepositoryLastUpdate<DtoEstoqueDepositoProdutoLote> _repositoryDepositoProdutoLoteAtributo;
        private MongoRepositoryLastUpdate<DtoEstoqueSaida> _repositoryEstoqueSaidaAtributo;
        private MongoRepositoryLastUpdate<DtoEstoqueEntrada> _repositoryEstoqueEntradaAtributo;
        private MongoRepository<DtoEstoqueUnidade> _repositoryEstoqueUnidadeAtributo;
        private MongoRepository<DtoEstoqueReserva> _repositoryEstoqueReservaAtributo;
        private MongoRepository<DtoEstoqueReservaProduto> _repositoryEstoqueReservaProdutoAtributo;
        private MongoRepository<DtoEstoqueMovEntreDepositos> _repositoryEstoqueMovEntreDepositosAtributo;
        private MongoRepository<DtoEstoqueMovEntreDepositosProduto> _repositoryEstoqueMovEntreDepositosProdutoAtributo;
        
        private MongoRepositoryLastUpdate<DtoEstoqueProdutoStatus> _repositoryEstoqueProdutoStatusAtributo;

        private MongoRepository<DtoEtiquetasPadroes> _repositoryEtiquetasPadroesAtributo;

        // COMPRA
        private MongoRepository<DtoOrdemCompraitem> _repositoryOrdemCompraItemAtributo;
        
        private MongoRepository<DtoOrdemCompra> _repositoryOrdemCompraAtributo;

        // ENTRADA
        private MongoRepository<DtoNotaEntradaProduto> _repositoryNotaEntradaProdutoAtributo;
        private MongoRepository<DtoNotaEntrada> _repositoryNotaEntradaAtributo;
        private MongoRepository<DtoNotaEntradaDuplicata> _repositoryNotaEntradaDuplicataAtributo;

        // VENDA
        private MongoRepositoryLastUpdate<DtoVendaProduto> _repositoryVendaProdutoAtributo;
        private MongoRepositoryLastUpdate<DtoVenda> _repositoryVendaAtributo;
        private MongoRepository<DtoNFesMultiVendas> _repositoryInitNFesMultiVendasAtributo;
        private MongoRepository<DtoVendaEquipamento> _repositoryVendaEquipamentoAtributo;
        private MongoRepositoryLastUpdate<DtoVendaAgrupamento> _repositoryVendaAgrupamentoAtributo;
        private MongoRepository<DtoVendaVeiculo> _repositoryVendaVeiculoAtributo;
        private MongoRepository<DtoVendaLaudo> _repositoryVendaLaudoAtributo;
        private MongoRepository<DtoVendaRapida> _repositoryVendaRapidaAtributo;
        private MongoRepository<DtoVendaStatus> _repositoryVendaStatusAtributo;
        private MongoRepository<DtoVendaTerceiros> _repositoryVendaTerceirosAtributo;
        private MongoRepositoryLastUpdate<DtoVendaCategoria> _repositoryVendaCategoriaAtributo;
        private MongoRepository<DtoVendaMeta> _repositoryVendaMetaAtributo;

        //EXPEDICAO
        private MongoRepository<DtoOrdemExpedicao> _repositoryOrdemExpedicaoAtributo;
        private MongoRepository<DtoOrdemExpedicaoItem> _repositoryOrdemExpedicaoItemAtributo;

        //PDV
        
        private MongoRepositoryLastUpdate<DtoVale> _repositoryValeAtributo;

        

        

        //FINANCEIRO
        private MongoRepositoryLastUpdate<DtoBanco> _repositoryBancoAtributo;
        private MongoRepositoryLastUpdate<DtoLancamento> _repositoryLancamentoAtributo;
        private MongoRepositoryLastUpdate<DtoCheque> _repositoryChequeAtributo;
        private MongoRepository<DtoGrupoDRE> _repositoryGrupoDREAtributo;
        private MongoRepositoryLastUpdate<DtoPlanoDeConta> _repositoryPlanoDeContaAtributo;
        private MongoRepositoryLastUpdate<DtoFormaPagamento> _repositoryFormaPagamentoAtributo;
        private MongoRepository<DtoCentroCustos> _repositoryCentroCustosAtributo;
        private MongoRepository<DtoLancamentoGrupo> _repositoryLancamentoGrupoAtributo;
        private MongoRepository<DtoTransferenciaBancaria> _repositoryTransferenciaBancariaAtributo;


        //PESSOAS
        private MongoRepositoryLastUpdate<DtoPessoa> _repositoryPessoaAtributo;
        private MongoRepository<DtoPessoaContato> _repositoryPessoaContatoAtributo;
        private MongoRepository<DtoPessoaGrupo> _repositoryPessoaGrupoAtributo;
        private MongoRepository<DtoProcesso> _repositoryProcessoAtributo;

        //EMPRESAS
        private MongoRepositoryLastUpdate<DtoEmpresa> _repositoryEmpresaAtributo;

        //MODULO MRP
        private MongoRepository<DtoEtapa> _repositoryEtapaAtributo;
        private MongoRepository<DtoProcessoProdutivo> _repositoryProcessoProdutivoAtributo;
        private MongoRepository<DtoProdutoQualidade> _repositoryProdutoQualidadeAtributo;
        private MongoRepository<DtoOrdemProducao> _repositoryOrdemProducaoAtributo;
        private MongoRepository<DtoOrdemProducaoHistorico> _repositoryOrdemProducaoHistoricoAtributo;
        private MongoRepository<DtoOrdemProducaoComposicao> _repositoryOrdemProducaoComposicaoAtributo;
        private MongoRepository<DtoOrdemProducaoStatus> _repositoryOrdemProducaoStatusAtributo;
        private MongoRepository<DtoOrdemProducaoCheckList> _repositoryOrdemProducaoCheckListAtributo;
        private MongoRepository<DtoLinhaProducao> _repositoryLinhaProducaoAtributo;


        //FISCAL        
        private MongoRepository<DtoLogNFe> _repositoryLogNFEAtributo;
        private MongoRepository<DtoLogNFSe> _repositoryLogNFSEAtributo;
        private MongoRepository<DtoLogMDFe> _repositoryLogMDFEAtributo;

        

        //NFSe
        private MongoRepository<DtoOrdemServico> _repositoryOrdemServicoAtributo;

        //CRM
        private MongoRepository<DtoOportunidade> _repositoryOportunidadeAtributo;
        private MongoRepository<DtoOportunidadeEvento> _repositoryOportunidadeEventoAtributo;
        
        //AGENDA
        private MongoRepository<DtoAgenda> _repositoryAgendaAtributo;



        //CONTRATO
        private MongoRepository<DtoContrato> _repositoryContratoAtributo;
        private MongoRepository<DtoTipoContrato> _repositoryContratoTipoAtributo;

        

        //PROJETO
        private MongoRepository<DtoProjeto> _repositoryProjetoAtributo;

        //COTAÇÃO
        private MongoRepository<DtoCotacao> _repositoryCotacaoAtributo;
        private MongoRepository<DtoCotacaoItem> _repositoryCotacaoItemAtributo;
        private MongoRepository<DtoCotacaoCotista> _repositoryCotacaoCotistaAtributo;
        private MongoRepository<DtoCotacaoItemCotado> _repositoryCotacaoItemCotadoAtributo;
        

        //Log
        private MongoRepository<Log> _repositoryLogAtributo;

        //Equipamentos
        private MongoRepositoryLastUpdate<DtoEquipamento> _repositoryEquipamentosAtributo;

        //Logs OS
        private MongoRepositoryLastUpdate<DtoLogsOrdemServico> _repositoryLogOrdemServicoAtributo;


        //Retornos Bancários PDF
        private MongoRepositoryLastUpdate<DtoRetornosBancarios> _repositoryRetornosBancariosAtributo;

        //Integrações

        private MongoRepository<DtoIntegracoes> _repositoryIntegracoesAtributo;

        private MongoRepository<DtoIntegracoesTempData> _repositoryIntegracoesTempDataAtributo;

        private MongoRepository<DtoIFoodAuthentication> _repositoryIFoodAuthenticationAtributo;

        private MongoRepository<DtoIFoodVenda> _repositoryIFoodVendaAtributo;

        #endregion
    }
}
#endregion
