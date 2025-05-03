using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLogRetornoRemessa : Entity
    {
        public DtoLogRetornoRemessa()
        {
            BoletosRetorno = new List<BoletoRetorno>();
        }

        public string Tipo { get; set; }

        public string Username { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        public string FileString { get; set; }

        public string BancoID { get; set; }

        public string FormatoRemessa { get; set; }

        public dynamic DetalhesRetorno { get; set; }

        public List<BoletoRetorno> BoletosRetorno { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataCompensacao { get; set; }

        public class BoletoRetorno
        {
            public string BoletoID { get; set; }

            public long BoletoCodigo { get; set; }

            public double ValorBoleto { get; set; }            

            public double ValorDetalhe { get; set; }
        }
    }
}
