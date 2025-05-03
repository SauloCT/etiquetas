using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    public class DtoCotacaoItemCotado : Entity
    {
        public bool Comprado { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorFrete { get; set; }
        public double ValorOutrasDespesas { get; set; }
        public string CotacaoID { get; set; }
        public string ItemID { get; set; }
        public string Item { get; set; }
        public string CotistaID { get; set; }
        public string Cotista { get; set; }
        public string Observacoes { get; set; }
        public bool Aprovado { get; set; }

        [BsonIgnore]
        public bool PodeAprovar { get; set; }
        [BsonIgnore]
        public string MenorPreco { get; set; }

    }
}
