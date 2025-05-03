using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaLaudo : Entity
    {
        public string VendaID { get; set; }

        public string OSID { get; set; }

        public string Tecnico { get; set; }

        public string Laudo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }
    }
}