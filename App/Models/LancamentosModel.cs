using X.PagedList;
using VendaERP.Core.Models;

namespace App.Models.Lancamentos
{
    public class LancamentosModel
    {
        public LancamentosModel() {
            autocompletar = new Autocompletar();
            filtros = new Filtros();
        }
        /// <summary>
        /// Codigo Sequencial dos lançamentos
        /// </summary>
        public Autocompletar autocompletar { get; set; }
        public Filtros filtros { get; set; }
        public List<DtoLancamento> listaLancamentos { get; set; }
        public string novoLancamento { get; set; }
    }
    /// <summary>
    /// Elementos para se utilizar para autocompletar campos
    /// </summary>
    public class Autocompletar
    {
        /// <summary>
        /// Lista de centros de custo
        /// </summary>
        public List<string> centrosDeCusto { get; set; }
        /// <summary>
        /// Lista de clientes
        /// </summary>
        public List<string> clientes { get; set; }
        /// <summary>
        /// Lista de contas bancarias
        /// </summary>
        public List<string> contasBancarias { get; set; }
        /// <summary>
        /// Lista de emails
        /// </summary>
        public List<string> emails { get; set; }
        /// <summary>
        /// Lista de empresas
        /// </summary>
        public List<string> empresas { get; set; }
        /// <summary>
        /// Lista de formas de pagamento
        /// </summary>
        public List<string> formasPagamento { get; set; }
        /// <summary>
        /// Lista de grupos
        /// </summary>
        public List<string> grupos { get; set; }
        /// <summary>
        /// Lista de planos de contas
        /// </summary>
        public List<string> planosDeConta { get; set; }
    }
    /// <summary>
    ///  Elementos para se utilizar para filtrar elementos
    /// </summary>
    public class Filtros
    {
        /// <summary>
        /// Variavel correspondente a CodigoSequencial da DtoLancamento
        /// </summary>
        public int codigo { get; set; }
        /// <summary>
        /// Variavel correspondente a Cliente da DtoLancamento
        /// </summary>
        public string cliente { get; set; }
        /// <summary>
        /// Variavel correspondente a PlanoDeConta da DtoLancamento
        /// </summary>
        public string planoDeConta { get; set; }
        /// <summary>
        /// Variavel correspondente a PlanoDeConta da DtoLancamento
        /// </summary>
        public string formaPagamento { get; set; }
        /// <summary>
        /// Variavel correspondente a Empresa da DtoLancamento
        /// </summary>
        public string empresa { get; set; }
        /// <summary>
        /// Variavel correspondente a Banco da DtoLancamento
        /// </summary>
        public string contaBancaria { get; set; }
        /// <summary>
        /// Variavel correspondente a LancamentoGrupo da DtoLancamento
        /// </summary>
        public string grupo { get; set; }
        /// <summary>
        /// Variavel correspondente a CentroDeCusto da DtoLancamento
        /// </summary>
        public string centroDeCusto { get; set; }
        /// <summary>
        /// Variavel correspondente a Entrada da DtoLancamento
        /// </summary>
        public double valor { get; set; }
        public string tipoLancamento { get; set; }
        public string situacaoLancamento { get; set; }
    }
}
