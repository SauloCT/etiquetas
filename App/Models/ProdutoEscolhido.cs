using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace App.Models
{
    public class ProdutoEscolhido
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }

        [BsonElement("CodigoNFe")]
        public string Codigo { get; set; }
        
        [BsonElement("Nome")]
        public string Nome { get; set; }
        
        [BsonElement("PrecoVenda")]
        public double Preco { get; set; }
        
        [BsonElement("Marca")]
        public string Marca { get; set; }
        
        [BsonElement("NumeroSerie")]
        public string NumeroSerie { get; set; }
        
        [BsonElement("EAN_NFe")]
        public string CodigoBarras { get; set; }
        
        [BsonIgnore]
        public string Lote { get; set; }
        
        [BsonIgnore]
        public int Quantidade { get; set; }
    }
} 