using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoPreco : EntityLastUpdate
    {
        [IndexedColumn("ProdutoID", "TabelaID")]
        public string ProdutoID { get; set; }

        [IndexedColumn()]
        public string TabelaID { get; set; }

        public string CodigoProduto { get; set; }

        public string Produto { get; set; }

        public double ComissaoVendedor { get; set; }

        public double ComissaoRepresentada { get; set; }

        public double MVA { get; set; }

        public double ST { get; set; }

        public double Despesas { get; set; }

        public double PrecoCusto { get; set; }

        public double PrecoVenda { get; set; }        

        [BsonIgnore]
        public double ComissaoVendedor_OLD { get; set; }

        [BsonIgnore]
        public double ComissaoRepresentada_OLD { get; set; }

        [BsonIgnore]
        public double PrecoVenda_OLD { get; set; }

        [BsonIgnore]
        public double MVA_OLD { get; set; }                

        [BsonIgnore]
        public string ProdutoCategoria { get; set; }

        [BsonIgnore]
        public string ProdutoMarca { get; set; }

        [BsonIgnore]
        public int MagentoProductId { get; set; }

        [BsonIgnore]
        public int WooCommerceProductId { get; set; }

        [BsonIgnore]
        public string TempId { get; set; }

        [BsonIgnore]
        public bool Visible { get; set; }

        [BsonIgnore]
        public bool HasChanges { get; set; }

    }
}