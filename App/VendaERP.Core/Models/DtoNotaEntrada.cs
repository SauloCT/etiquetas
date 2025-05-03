

using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoNotaEntrada : Entity
    {

        [AutoIncrement]

        public int Codigo { get; set; }

        public string EmpresaID { get; set; }



        public string Empresa { get; set; }

        public string DepositoID { get; set; }



        public string Deposito { get; set; }

        public string FornecedorID { get; set; }



        public string Fornecedor { get; set; }

        public string TransportadoraID { get; set; }



        public string Transportadora { get; set; }

        public bool RatearFreteSeguro { get; set; }

        // financeiro
        public string PlanoDeContaID { get; set; }

        public string PlanoDeConta { get; set; }

        public string CentroDeCustoID { get; set; }

        public string CentroDeCusto { get; set; }

        public string FormaDePagamentoID { get; set; }

        public string FormaDePagamento { get; set; }

        public CondicaoPagamento CondicaoPagamento { get; set; }

        public int Parcelas { get; set; }

        public int PeriodoParcelas { get; set; }

        public double Adiantamento { get; set; }

        // dados da nota
        public string ModeloNota { get; set; }



        public string NumeroNota { get; set; }



        public string SerieNota { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataEmissaoNota { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataEntradaNota { get; set; }

        public string RegimeTributarioEmitente { get; set; }

        public string Operacao { get; set; }



        public string CFOPCapa { get; set; }



        public bool NotaServicos { get; set; }

        public bool NotaSemItens { get; set; }



        public bool Lancado { get; set; }



        public bool Finalizado { get; set; }

        public bool MovimentacaoEstoqueEmAndamento { get; set; }

        public bool RatearICMSST { get; set; }

        public double OutrasDespesas { get; set; }

        public double Desconto { get; set; }

        public double ValorFrete { get; set; }

        public double ValorSeguro { get; set; }


        public double ValorNota { get; set; }



        public double ValorTotalDaNota { get; set; }

        public double ICMSBase { get; set; }

        public double ICMSValor { get; set; }

        public double ICMS_STValor { get; set; }

        public double COFINSBase { get; set; }

        public double COFINSValor { get; set; }

        public double PISBase { get; set; }

        public double PISValor { get; set; }

        public double ISSBase { get; set; }

        public double ISSValor { get; set; }

        public double IPIValor { get; set; }

        public double IPIBase { get; set; }

        public string Descricao { get; set; }

        public string ModalidadeFrete { get; set; }

        public string InformacoesAdicionais { get; set; }

        public string XML { get; set; }

        public bool ManterValoresXML { get; set; }



        public string ChaveAcesso { get; set; }

        public bool Importado { get; set; }

        [BsonIgnore]
        public List<DtoNotaEntradaProduto> Produtos { get; set; }

        public List<DtoNotaEntradaDuplicata> Duplicatas { get; set; }

        public bool NotaNaoMovimentoEstoque { get; set; }

    }
}
