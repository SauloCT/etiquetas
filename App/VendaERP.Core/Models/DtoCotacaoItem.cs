
using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    public class DtoCotacaoItem : Entity
    {
        public string CotacaoID { get; set; }

        public string ProdutoID { get; set; }

        public string CodigoNFe { get; set; }

        public string Descricao { get; set; }

        public double Quantidade { get; set; }

        public bool Comprado { get; set; }

        public AprovadorItem Aprovador { get; set; }

        [BsonIgnore]
        public DtoProdutoSimilar[] ProdutosSimilares { get; set; }
    }

    public class AprovadorItem
    {
        //Para nao confundir, esse verificado não é do produto,
        //o aprovador pode aprovar compras sem algum produto, basicamente é uma bool que diz se 
        // o aprovador "analisou" o produto cotado, porem não necessariamente é obrigado a aprovar ele
        public bool Verificado { get; set; }
        public string Nome { get; set; }
        public string UserId { get; set; }
        public StatusAprovador StatusAprovador { get; set; }
    }

    public enum StatusAprovador
    {
        Aprovou,
        Recusou
    }
}
