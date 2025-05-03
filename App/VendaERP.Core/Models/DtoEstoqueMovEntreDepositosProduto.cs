using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueMovEntreDepositosProduto : Entity
    {
        public string MovimentacaoID { get; set; }

        public string ProdutoID { get; set; }

        public string ProdutoCodigoNFe { get; set; }

        public string Descricao { get; set; }

        public double Quantidade { get; set; }

        public string Lote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }
    }
}