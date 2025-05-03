

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemCompra : Entity
    {
        [AutoIncrement]

        public int Codigo { get; set; }

        [IndexedColumn]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Data { get; set; }


        public string EmpresaID { get; set; }


        public string Empresa { get; set; }

        public string DepositoID { get; set; }


        public string Deposito { get; set; }


        public string Operacao { get; set; }


        public string Situacao { get; set; }


        public string AlteradoPor { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataHoraAlteracao { get; set; }

        public string FornecedorID { get; set; }


        public string Fornecedor { get; set; }


        public string FornecedorEmail { get; set; }


        public string AprovacaoEmail { get; set; }

        public string PlanoDeContaID { get; set; }

        public string PlanoDeConta { get; set; }

        public string CentroDeCustoID { get; set; }

        public string CentroDeCusto { get; set; }

        public string FormaDePagamentoID { get; set; }

        public string FormaDePagamento { get; set; }

        /// <summary>
        /// Aqui é para ser o número da NF de Entrada
        /// </summary>
        /// 
        

        public string NotaFiscal { get; set; }

        /// <summary>
        /// Aqui é para ser o código da NF de Entrada
        /// </summary>
        public string NotaEntradaID { get; set; }

        public int Parcelas { get; set; }

        public double Adiantamento { get; set; }

        public double ValorSemFrete { get; set; }


        public double ValorTotal { get; set; }

        public double ValorMinimoCompra { get; set; }

        [IndexedColumn]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime PrevisaoEntrega { get; set; }

        public string FreteTransportadoraID { get; set; }

        public string FreteTransportadora { get; set; }

        public string FretePorConta { get; set; }

        public double FreteValor { get; set; }

        public double DespesasValor { get; set; }

        public string EntregaPais { get; set; }

        public string EntregaCEP { get; set; }

        public string EntregaUF { get; set; }

        public string EntregaUFCodigo { get; set; }

        public string EntregaCidade { get; set; }

        public string EntregaCidadeCodigo { get; set; }

        public string EntregaBairro { get; set; }

        public string EntregaLogradouro { get; set; }

        public string EntregaNumero { get; set; }

        public string EntregaComplemento { get; set; }

        public string Observacoes { get; set; }

        public bool Lancado { get; set; }

        public string FaturamentoDetalhes { get; set; }

        // lista em http://pt.wikipedia.org/wiki/Anexo:Lista_de_moedas
        public string CodigoISOMoeda { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataEmailRecebido { get; set; }

        public bool EmailRecebido { get; set; }

        public string DataRecebimentoEmail { get; set; }

        public bool OcultarCampos { get; set; }

        public string NotadeEntradaID { get; set; }

        public string CotacaoID { get; set; }
    }
}