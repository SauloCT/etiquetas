using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProcesso : Entity
    {
        public string Nome { get; set; }

        public bool Bloqueado { get; set; }

        public string JSON { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataModificacao { get; set; }

        public string ModificadoPor { get; set; }

        public int Versao { get; set; }

        public int CondicaoInicial { get; set; }


    }
}
