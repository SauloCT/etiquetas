using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueSaida : EntityLastUpdate
    {
        public double CodigoProduto { get; set; }

        public string CodigoProdutoNFE { get; set; }

        public string OrdemProducaoID { get; set; }

        public string MovimentacaoEntreEstoquesID { get; set; }

        [IndexedColumn()]
        public string VendaID { get; set; }

        public string VendaConsignadaID { get; set; }

        public string VendaRapidaID { get; set; }

        [IndexedColumn()]
        public string ProdutoID { get; set; }
        
        [IndexedColumn()]
        public string DepositoID { get; set; }

        [AutoIncrement]
        public int Codigo { get; set; }

        public string Produto { get; set; }

        public string Deposito { get; set; }

        public string NF { get; set; }

        //Se foi uma venda, compra... transferência
        public string Movimentacao { get; set; }

        public double Quantidade { get; set; }

        public string Unidade { get; set; }

        public double ValorUnitario { get; set; }

        public double ValorTotal { get; set; }

        public string Observacoes { get; set; }

        [IndexedColumn()]
        public string Cliente { get; set; }
        
        public string ClienteID { get; set; }

        [IndexedColumn()]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        public string Lote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime FabricacaoLote { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        public string VendaProdutoID { get; set; }

        public string NotaEntradaID { get; set; }

        public bool ScriptCorrecao { get; set; }

        public List<AtributosLote> Lotes { get; set; }
    }
}