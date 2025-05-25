using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VendaERP.Core;

namespace App.Models
{
    public class Autocompletar
    {
        /// <summary>
        /// Lista de clientes
        /// </summary>
        public List<Cliente> clientes { get; set; }
        /// <summary>
        /// Lista de empresas
        /// </summary>
        public List<Empresa> empresas { get; set; }
        public List<TabelaDePreco> tabelaDePreco { get; set; }
        public List<Deposito> depositos { get; set; }
        public List<ProdutoAutocompletar> produtos { get; set; }
        public List<ModeloEtiqueta> modelosEtiquetas { get; set; }
        
        public Autocompletar(DBAccess db)
        {
            var _db = db;
            
            var clientesBson = _db._repositoryPessoa.Collection.Aggregate().Project(new BsonDocument{{"_id", true},{"NomeFantasia", true},{"Email", true } }).Sort("{NomeFantasia:1}").ToList();
            if(clientesBson != null)
                clientes = BsonSerializer.Deserialize<List<Cliente>>(clientesBson.ToJson());
                
            var empresaBson = _db._repositoryEmpresa.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "NomeFantasia", true } }).Sort("{RazaoSocial:1}").ToList();
            if (empresaBson != null)
                this.empresas = BsonSerializer.Deserialize<List<Empresa>>(empresaBson.ToJson());
                
            var tabelaDePrecoBson = _db._repositoryProdutoTabelaPreco.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
            if (tabelaDePrecoBson != null)
                this.tabelaDePreco = BsonSerializer.Deserialize<List<TabelaDePreco>>(tabelaDePrecoBson.ToJson());
                
            var depositoBson = _db._repositoryDeposito.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
            if (depositoBson != null)
                this.depositos = BsonSerializer.Deserialize<List<Deposito>>(depositoBson.ToJson());
                
            var produtosBson = _db._repositoryProduto.Collection.Aggregate().Project(new BsonDocument { 
                { "_id", true }, 
                { "Nome", true },
                { "CodigoNFe", true }
            }).Sort("{Nome:1}").ToList();
            if (produtosBson != null)
                this.produtos = BsonSerializer.Deserialize<List<ProdutoAutocompletar>>(produtosBson.ToJson());
                
            var modelosEtiquetasBson = _db._repositoryEtiquetasPadroes.Collection.Aggregate().Project(new BsonDocument { { "_id", true }, { "Nome", true } }).Sort("{Nome:1}").ToList();
            if (modelosEtiquetasBson != null)
                this.modelosEtiquetas = BsonSerializer.Deserialize<List<ModeloEtiqueta>>(modelosEtiquetasBson.ToJson());
        }
    }
    
    public class Cliente
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
    }
    
    public class Empresa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("NomeFantasia")]
        public string Nome { get; set; }
    }
    
    public class TabelaDePreco
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; }
    }
    
    public class Deposito
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; }
    }
    
    public class ProdutoAutocompletar
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; }
        [BsonElement("CodigoNFe")]
        public string CodigoNFe { get; set; }
    }
    
    public class ModeloEtiqueta
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; }
    }
}
