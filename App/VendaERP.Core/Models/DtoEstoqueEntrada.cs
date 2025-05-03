using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueEntrada : EntityLastUpdate
    {
        public double CodigoProduto { get; set; }

        public string CodigoProdutoNFE { get; set; }

        public string NotaEntradaID { get; set; }

        public string OrdemCompraID { get; set; }

        public string MovimentacaoEntreEstoquesID { get; set; }

        public string CompraRapidaID { get; set; }

        public string VendaConsignadaID { get; set; }

        [IndexedColumn()]
        public string VendaID { get; set; }

        [IndexedColumn()]
        public string ProdutoID { get; set; }

        [IndexedColumn()]
        public string DepositoID { get; set; }

        public string OrdemProducaoID { get; set; }

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

        public double ValorFrete { get; set; }

        public double ValorSeguro { get; set; }

        public double ValorICMS { get; set; }

        public double ValorIPI { get; set; }

        public string Observacoes { get; set; }

        [IndexedColumn()]
        public string Fornecedor { get; set; }

        public string FornecedorID { get; set; }

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

        public bool ScriptCorrecao { get; set; }

    }
}