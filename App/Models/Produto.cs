using MongoDB.Bson.Serialization.Attributes;

namespace App.Models
{
    public class Produto
    {
        [BsonId]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CodigoNFe { get; set; }
    }
} 