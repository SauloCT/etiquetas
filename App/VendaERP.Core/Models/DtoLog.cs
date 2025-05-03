using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class Log : Entity
    {
        public string Usuario { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        public string Acao { get; set; }

        public string Tela { get; set; }

        public string Query { get; set; }
    }
}