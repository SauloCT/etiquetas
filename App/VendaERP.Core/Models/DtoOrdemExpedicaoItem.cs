using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemExpedicaoItem : Entity
    {
        public string OrdemExpedicaoID { get; set; }

        public string VendaProdutoID { get; set; }

        public string CodigoVenda { get; set; }

        public string ClienteID { get; set; }

        public string Cliente { get; set; }

        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public string UnidadeMedida { get; set; }

        public string Dimensoes { get; set; }

        public double Peso { get; set; }

        public string Prateleira { get; set; }

        public string ProdutoDescricao { get; set; }

        public string Lote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerieExpedidos { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        public double QuantidadeExpedida { get; set; }

        [BsonIgnore]
        public double MaximoExpedivel { get; set; }

        [BsonIgnore]
        public double QuantidadeTotalDoItemNoPedido { get; set; }

        [BsonIgnore]
        public double QuantidadeJaExpedidaEmOutrasOrdens { get; set; }

        [BsonIgnore]
        public double QuantidadeASerExpedida { get; set; }
        
        public string Especificacao { get; set; }
    }
}
