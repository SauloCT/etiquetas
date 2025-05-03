using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueDepositoProdutoLote : EntityLastUpdate
    {
        public string Produto { get; set; }

        [IndexedColumn("ProdutoID", "DepositoID")]
        public string ProdutoID { get; set; }

        public string Deposito { get; set; }

        public string DepositoID { get; set; }

        //[ColumnProperties("left", "Compra Máxima de Itens", ColumnVisibility = false)]
        //public double CompraMaxima { get; set; }

        //[ColumnProperties("left", "Estoque Minimo", ColumnVisibility = false)]
        //public double EstoqueMinimo { get; set; }

        public double Saldo { get; set; }

        public string Lote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime DataFabricacao { get; set; }
    }
}