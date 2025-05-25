using MongoDB.Bson.Serialization.Attributes;

namespace App.Models
{
    public class Produto
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string? CodigoNFe { get; set; }
    }
} 