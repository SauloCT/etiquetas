


using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVale : EntityLastUpdate
    {
        [AutoIncrement, Key]

        public long Codigo { get; set; }


        public string Cliente { get; set; }


        public string Empresa { get; set; }



        public double Valor { get; set; }


        public double? ValorUsado { get; set; }

        public DateTime? DataUsado { get; set; }


        public long? CupomPai { get; set; }


        public long? CupomFilho { get; set; }


        public DateTime? Validade { get; set; }


        public long CodigoVenda { get; set; }


        public StatusVale Status { get; set; }

        public string ClienteID { get; set; }


        public string EmpresaID { get; set; }

        public string VendaID { get; set; }

        public List<ProdutoDevolvido> Produtos { get; set; }

        public string Descricao { get; set; }

        public int? CodigoNotaFiscal { get; set; }

        public string NotaFiscalID { get; set; }

        [BsonIgnore]
        public string UrlImpressao { get; set; }
    }

    public enum StatusVale
    {
        USADO = 0,
        DISPONIVEL = 1,
        VENCIDO = 2,
        VINCULADO = 3,
        TODOS = 4
    }

    public class ProdutoDevolvido
    {
        public string ProdutoID { get; set; }
        public string Produto { get; set; }
        public double ValorDevolvido { get; set; }
        public double QuantidadeDevolvida { get; set; }
        public string Descricao { get; set; }
    }

}
