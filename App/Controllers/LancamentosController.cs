using App.Models.Lancamentos;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using VendaERP.Core;
using VendaERP.Core.Models;
using X.PagedList;

namespace App.Controllers
{
    public class LancamentosController : Controller
    {
        ///<summary>
        ///Variavel padrão de acesso ao Banco de Dados
        ///</summary>
        private readonly DBAccess _db;
        private LancamentosModel _model;

        public LancamentosController(DBAccess db)
        {
            this._db = db;
            this._model = new LancamentosModel();
        }
        public IActionResult Index(int? codigo, string? cliente, string? planoDeConta, string? formaPagamento, string? empresa, string? contaBancaria, string? grupo, string? centroDeCusto, double? valor, string? tipoLancamento, string? situacaoLancamento, int pageNumber = 1, int pageSize = 15)
        {
            Autocompletar();
            if (this._model.filtros == null)
                this._model.filtros = new Filtros();

            #region Filtros
            var filters = Builders<DtoLancamento>.Filter.Where(x => x.LancamentoPaiId == null);

            //Filtro correspondente a CodigoSequencial da DtoLancamento
            _model.filtros.codigo = codigo ?? -1;
            if (_model.filtros.codigo > -1)
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.CodigoSequencial == _model.filtros.codigo);

            //Filtro correspondente ao CodigoSequencial da DtoLancamento
            _model.filtros.cliente = cliente ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.cliente))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Cliente == _model.filtros.cliente);

            //Filtro correspondente ao PlanoDeConta da DtoLancamento
            _model.filtros.planoDeConta = planoDeConta ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.planoDeConta))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.PlanoDeConta == _model.filtros.planoDeConta);

            //Filtro correspondente ao FormaPagamento da DtoLancamento
            _model.filtros.formaPagamento = formaPagamento ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.formaPagamento))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.FormaPagamento == _model.filtros.formaPagamento);

            //Filtro correspondente ao Empresa da DtoLancamento
            _model.filtros.empresa = empresa ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.empresa))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Empresa == _model.filtros.empresa);

            //Filtro correspondente ao Banco da DtoLancamento
            _model.filtros.contaBancaria = contaBancaria ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.contaBancaria))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Banco == _model.filtros.contaBancaria);

            //Filtro correspondente ao LancamentoGrupo da DtoLancamento
            _model.filtros.grupo = grupo ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.grupo))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.LancamentoGrupo == _model.filtros.grupo);

            //Filtro correspondente ao CentroDeCusto da DtoLancamento
            _model.filtros.centroDeCusto = centroDeCusto ?? "";
            if (!string.IsNullOrEmpty(_model.filtros.centroDeCusto))
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.CentroDeCusto == _model.filtros.centroDeCusto);

            //Filtro correspondente ao CentroDeCusto da DtoLancamento
            _model.filtros.valor = valor ?? -1;
            if (_model.filtros.valor > -1)
                filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Entrada == _model.filtros.valor);

            _model.filtros.tipoLancamento = tipoLancamento ?? "Tudo";
            if (!string.IsNullOrEmpty(_model.filtros.tipoLancamento))
                switch (_model.filtros.tipoLancamento)
                {
                    case "Tudo":
                        break;
                    case "Despesa":
                        filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Despesa == true);
                        break;
                    case "Receita":
                        filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Despesa == false);
                        break;
                    case "Comissões de Vendedores":
                        filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Observacoes == "Comissão do Vendedor - Gerado automaticamente através do modulo PDV");
                        break;
                }

            _model.filtros.situacaoLancamento = situacaoLancamento ?? "Todas";
            if (!string.IsNullOrEmpty(_model.filtros.situacaoLancamento))
                switch (_model.filtros.situacaoLancamento)
                {
                    case "Todas":
                        break;
                    case "Quitado":
                        filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Pago == true);
                        break;
                    case "Não Quitado":
                        filters = filters & Builders<DtoLancamento>.Filter.Where(x => x.Pago == false);
                        break;
                }
            #endregion

            var LancamentosList = _db._repositoryLancamento.Collection.Aggregate().Match(filters).ToList().OrderByDescending(x => x.DataModificacao).ToList();

            #region Variaveis de controle de paginação
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 15)
                pageSize = 15;
            else if (pageSize > 100)
                pageSize = 100;
            #endregion

            var pager = new Pager(LancamentosList.Count(), pageNumber, pageSize);
            _model.listaLancamentos = LancamentosList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View(_model);
        }
        public IActionResult NovoLancamento(string? empresa, string? cliente, string? planoDeConta, double? valor,string? formaPagamento, string? contaBancaria, DateTime? dataCompetencia, DateTime? dataVencimento, bool? foiQuitado, string? centroDeCusto, double? desconto, double? descontoDinheiro, double? descontoAteVencimento, double? entrada, string? numeroOuNomeDocumento, string? grupo, double? jurosAntecipacao, double? jurosDinheiroAntecipacao, bool? foiConciliado, string? email, string? descricao, string? observacao)
        {
            Autocompletar();
            if (empresa != null && cliente != null && planoDeConta != null && valor != null && email != null)
            {
                var codigoSequencial = _db._repositoryLancamento.Collection.Find<DtoLancamento>(x => true).ToList().OrderByDescending(x => x.CodigoSequencial).FirstOrDefault().CodigoSequencial;
                var plano = _db._repositoryPlanoDeConta.Collection.Find<DtoPlanoDeConta>(x => x.Nome == planoDeConta).FirstOrDefault();
                DtoLancamento novoLancamento = new DtoLancamento {
                    Banco = contaBancaria != null ? contaBancaria : null,
                    BancoID = contaBancaria != null ? _db._repositoryBanco.Collection.Find<DtoBanco>(x => x.Nome == contaBancaria).FirstOrDefault().Id : null,
                    CentroDeCusto = centroDeCusto != null ? centroDeCusto : null,
                    CentroDeCustoID = centroDeCusto != null ? _db._repositoryCentroCusto.Collection.Find<DtoCentroCustos>(x => x.Nome == centroDeCusto).FirstOrDefault().Id : null,
                    Cliente = cliente,
                    ClienteID = _db._repositoryPessoa.Collection.Find<DtoPessoa>(x => x.NomeFantasia == cliente).FirstOrDefault().Id,
                    CodigoSequencial = codigoSequencial >= 0 ? codigoSequencial + 1: 1,
                    Conciliado = foiConciliado ?? false,
                    DataCompetencia = dataCompetencia ?? DateTime.Now,
                    DataFluxo = DateTime.Now.Date,
                    DataModificacao = DateTime.Now,
                    DataPagamento = foiQuitado == true ? DateTime.Now : DateTime.MinValue,
                    DataVencimento = dataVencimento ?? DateTime.Now,
                    DataVencimentoOriginal = dataVencimento ?? DateTime.Now,
                    Desconto = desconto ?? 0.00,
                    DescontoDinheiro = descontoDinheiro ?? 0.00,
                    Descricao = "Despesa referente ao plano de conta " + plano.Hierarquia + " - " + plano.Nome,
                    Despesa = plano.EhDespesa,
                    Empresa = empresa,
                    EmpresaID = _db._repositoryEmpresa.Collection.Find<DtoEmpresa>(x => x.RazaoSocial == empresa).FirstOrDefault().Id,
                    Entrada = plano.EhDespesa ? 0.00 : valor ?? 0.00,
                    LancamentoGrupo = grupo ?? null,
                    LancamentoGrupoID = grupo != null ? _db._repositoryLancamentoGrupo.Collection.Find<DtoLancamentoGrupo>(x => x.Nome == grupo).FirstOrDefault().Id : null,
                    LastUpdate = DateTime.Now,
                    ModificadoPor = email != null ? email + " em " + DateTime.Now.ToString("dd/MM/yyy HH:mm") : null,
                    NumeroDocumento = numeroOuNomeDocumento ?? "L " + codigoSequencial,
                    PlanoDeConta = planoDeConta,
                    PlanoDeContaID = plano.Id,
                    Saida = plano.EhDespesa ? valor ?? 0.00 : 0.00,
                    
                };
                _db._repositoryLancamento.Collection.InsertOne(novoLancamento);
                _model.novoLancamento = "true";
            }
            else
            {
                _model.novoLancamento = "false";
            }
            return View(_model);
        }
        /// <summary>
        /// Função de auto completar
        /// </summary>
        private void Autocompletar()
        {
            if (this._model.autocompletar == null)
                this._model.autocompletar = new App.Models.Lancamentos.Autocompletar();
            #region Auto completar filtros
            if (this._model.autocompletar.clientes == null || this._model.autocompletar.emails == null)
            {
                var pessoas = _db._repositoryPessoa.Collection.Find<DtoPessoa>(_ => true).ToList();
                this._model.autocompletar.clientes = pessoas.OrderBy(x => x.NomeFantasia).Select(x => x.NomeFantasia).Distinct().ToList();
                this._model.autocompletar.emails = pessoas.OrderBy(x => x.Email).Select(x => x.Email).Distinct().ToList();
            }
            if (this._model.autocompletar.empresas == null)
                this._model.autocompletar.empresas = _db._repositoryEmpresa.Collection.Find<DtoEmpresa>(_ => true).ToList().OrderBy(x => x.RazaoSocial).Select(x => x.RazaoSocial).Distinct().ToList();
            if (this._model.autocompletar.formasPagamento == null)
                this._model.autocompletar.formasPagamento = _db._repositoryFormaPagamento.Collection.Find<DtoFormaPagamento>(_ => true).ToList().OrderBy(x => x.Nome).Select(x => x.Nome).Distinct().ToList();
            if (this._model.autocompletar.planosDeConta == null)
                this._model.autocompletar.planosDeConta = _db._repositoryPlanoDeConta.Collection.Find<DtoPlanoDeConta>(_ => true).ToList().OrderBy(x => x.Nome).Select(x => x.Nome).Distinct().ToList();
            if (this._model.autocompletar.contasBancarias == null)
                this._model.autocompletar.contasBancarias = _db._repositoryBanco.Collection.Find<DtoBanco>(_ => true).ToList().OrderBy(x => x.Nome).Select(x => x.Nome).Distinct().ToList();
            if (this._model.autocompletar.grupos == null)
                this._model.autocompletar.grupos = _db._repositoryLancamentoGrupo.Collection.Find<DtoLancamentoGrupo>(_ => true).ToList().OrderBy(x => x.Nome).Select(x => x.Nome).Distinct().ToList();
            if (this._model.autocompletar.centrosDeCusto == null)
                this._model.autocompletar.centrosDeCusto = _db._repositoryCentroCusto.Collection.Find<DtoCentroCustos>(_ => true).ToList().OrderBy(x => x.Nome).Select(x => x.Nome).Distinct().ToList();
            #endregion
        }
    }
}
