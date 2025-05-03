using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducaoHistorico : Entity
    {
        public string OrdemProducaoID { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }
        
        public string Descricao { get; set; }

        public string Status { get; set; }

        public string Usuario { get; set; }
    }
}