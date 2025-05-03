

using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;


namespace VendaERP.Core.Models
{
    public sealed class DtoLancamento : EntityLastUpdate
    {
        [IndexedColumn()]
        public string LancamentoPaiId { get; set; }

        [IndexedColumn()]
        public string EmpresaID { get; set; }

        public string Empresa { get; set; }

        [IndexedColumn()]
        public string ClienteID { get; set; }



        public string Cliente { get; set; }


        [AutoIncrement]
        [IndexedColumn()]

        public int CodigoSequencial { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataFluxo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [IndexedColumn()]

        public DateTime DataVencimento { get; set; }

        public bool Pago { get; set; }


        public bool Conciliado { get; set; }



        public string Situacao { get; set; }

        public string PlanoDeContaID { get; set; }



        public string PlanoDeConta { get; set; }

        public string CentroDeCustoID { get; set; }

        public string CentroDeCusto { get; set; }

        public string Descricao { get; set; }

        public string BancoID { get; set; }



        public string Banco { get; set; }

        public string FormaPagamentoID { get; set; }

        public string FormaPagamento { get; set; }

        public double Entrada { get; set; }

        public double Saida { get; set; }

        public double SaldoAtual { get; set; }

        public double Desconto { get; set; }

        public double DescontoDinheiro { get; set; }


        public double Recebido { get; set; }

        public bool Despesa { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataModificacao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataPagamento { get; set; }

        public string Observacoes { get; set; }
        
        [IndexedColumn()]
        public string VendaID { get; set; }

        public string OrdemServicoID { get; set; }

        public string EntradaEstoqueID { get; set; }

        public string ConsumoID { get; set; }

        public string VendaRapidaID { get; set; }

        public string CompraRapidaID { get; set; }

        public string ContratoID { get; set; }

        public string NFSeID { get; set; }

        public long NFSeNumero { get; set; }

        public int RPSNumero { get; set; }

        public string NotaEntradaID { get; set; }

        public string FolhaPagamentoID { get; set; }

        public bool RemessaPagamentoGerada { get; set; }

        public bool RetornoRemessaPagamento { get; set; }

        [IndexedColumn()]
        public string LancamentoParcelamentoID { get; set; }

        public string FrotaAbastecimentoID { get; set; }

        public string FrotaTrocaOleoID { get; set; }

        public string FrotaServicoID { get; set; }

        public string FrotaDespesaID { get; set; }

        public string FrotaSeguroID { get; set; }



        public long NumeroBoleto { get; set; }

        public double Multa { get; set; }

        public double Juro { get; set; }

        ///<summary>
        ///No lançamento Pai é a soma dos valores de todos os pagamentos, no pagamento vai Valor + multa + juros
        ///</summary>
        public double ValorPago { get; set; }
        ///<summary>
        ///Criado para poder colocar ali numero de nota fiscal, ou outra coisa que originou o lançamento
        ///</summary>
        public string NumeroDocumento { get; set; }

        public double Adiantamento { get; set; }

        public string LancamentoGrupoID { get; set; }

        public string LancamentoGrupo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVencimentoOriginal { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataCompetencia { get; set; }

        public string CriadoPor { get; set; }

        public string ModificadoPor { get; set; }

        public bool ConciliarAutomaticamente { get; set; }


        public TipoRelacionamentoLancamento TipoRelacionamento { get; set; }

        public bool PodeVincularComissaoVendedor { get; set; }

        /// <summary>
        /// Víncular Comissão do Vendedor com o Faturamento
        /// </summary>
        public bool VincularComissaoVendedor { get; set; }

        /// <summary>
        /// Vínculo da Comissão do Vendedor com o Faturamento
        /// </summary>
        [IndexedColumn()]
        public String LancamentoFaturamentoID { get; set; }

        /// <summary>
        /// Ao pagar o Faturamento não pagar automaticamente comissão vinculada
        /// </summary>
        public bool NaoPagarComissaoVinculadaAutomaticamente { get; set; }


        public string TransacaoOFXID { get; set; }

        public string CredenciadoraID { get; set; }

        public string BandeiraCartao { get; set; }

        public double ValorLiquido { get; set; }

        /// <summary>
        /// Introduzido em 15/03/2018. Para vendas geradas com duplicatas, a fim de manter os números e sincronizar corretamente com a venda.
        /// </summary>
        public string NumeroDuplicata { get; set; }

        /// <summary>
        /// Introduzido em 15/03/2018. Para vendas geradas com pagamentos do tipo cartão, a fim de manter os números e sincronizar corretamente com a venda.
        /// </summary>
        public string CV_NSU { get; set; }

        /// <summary>
        /// Introduzido em 29/03/2018. Para vendas geradas com pagamentos do tipo cartão, a fim de manter os números e sincronizar corretamente com a venda.
        /// </summary>
        public string NumeroTerminal { get; set; }

        #region Auxiliares NÃO SALVOS EM BANCO referentes a necessidades com o angularJS
        [BsonIgnore]
        public string Tipo { get; set; }

        [BsonIgnore]
        public double Valor { get; set; }

        [BsonIgnore]
        public ModoParcelamento ModoParcelamento { get; set; }

        [BsonIgnore]
        public int NumeroParcelas { get; set; }

        [BsonIgnore]
        public IntervaloParcelamento Intervalo { get; set; }

        [BsonIgnore]
        public int DiasIntervalo { get; set; }

        [BsonIgnore]
        public bool JurosCompostos { get; set; }

        [BsonIgnore]
        public DtoLancamento[] _ParcelasRecorrencias { get; set; }

        [BsonIgnore]
        public double ValorQuitar { get; set; }

        [BsonIgnore]
        public int Pagamentos { get; set; }

        [BsonIgnore]

        public int PagamentosConciliados { get; set; }

        [BsonIgnore]
        public List<DtoLancamento> PagamentosList { get; set; }

        [BsonIgnore]
        public DtoLancamento NewPagamento { get; set; }

        /// <summary>
        /// Setado ao carregar a tela de edição, para evitar que seja salvo depois por cima, precisa ser assim, por que em data o angular tira os segundos aí não fica 100% confiável
        /// </summary>
        [BsonIgnore]
        public long DataModificacaoTicks { get; set; }
        #endregion

        #region Coisa Velha Unútil Que não foi tirada para não dar Erro
        [Obsolete("Foi Substituido por TipoRelacionamentoLancamento")]
        [BsonIgnore]
        public bool IsLancamentoComissaoVendedor { get; set; }

        [Obsolete("Foi Substituido por TipoRelacionamentoLancamento")]
        [BsonIgnore]
        public bool IsLancamentoFrete { get; set; }
        #endregion

        public double CalcularJurosEmReais() => (Despesa ? Saida : Entrada) * (Juro / 100);

        public bool TaxaMontagem { get; set; }

        /// <summary>
        /// Introduzido em 09/01/2020 - para identificar nos parceiros WL do sige que caso o lançamento é referente a uma licenca, 
        /// ao quitar o sistema deve buscar na DtoTransacaoSistema e na DtoLicencaSistema para dar a baixa na licenca do cliente do parceiro WL.
        /// </summary>
        public bool EhLancamentoLicencaClienteParceiroWL { get; set; }
    }

    public enum TipoRelacionamentoLancamento
    {
        Faturamento = 0,
        Frete = 1,
        ComissaoVendedor = 2,
        Manual = 3,
        Devolucao = 4,
        TaxaCredenciadora = 5,
        TaxaIntegracao = 6,
        Troca = 7,
        FaturamentoParcial = 10
    }

    public enum ModoParcelamento
    {
        NaoParcelar = 0,
        ParcelamentoAutomatico = 1,
        RecorrenciaAutomatica = 2,
        ParcelamentoManual = 3,
    }

    public enum IntervaloParcelamento
    {
        Meses = 1,
        Dias = 2,
        PeriodosParcelamento = 3
    }
}