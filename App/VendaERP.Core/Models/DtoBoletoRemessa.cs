using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoBoletoRemessa : Entity
    {
        public int NumeroRemessa { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataRemessa { get; set; }

        public string Banco { get; set; }

        public string BancoID { get; set; }

        public int TipoArquivo { get; set; }

        public string Status { get; set; }
    }
}