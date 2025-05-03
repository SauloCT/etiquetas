using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoTipoContrato : Entity
    {
        [Key("TipoContrato")]

        public string Nome { get; set; }


        public bool PossuiTemplate { get; set; }

        public byte[] Template { get; set; }


        public string TemplateName { get; set; }

        [BsonIgnore]
        public string ArquivoTemporarioID { get; set; }

        public double Comissao { get; set; }
    }
}