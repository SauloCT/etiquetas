

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLogNFe : Entity
    {

        public string Usuario { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [IndexedColumn]

        public DateTime Data { get; set; }


        public string Acao { get; set; }


        public string TipoEvento { get; set; }

        public bool EhNFCe { get; set; }

        public string xmlRetorno { get; set; }

        public string xmlEnviado { get; set; }
    }
}