using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOportunidadeEvento : Entity
    {
        public string ClienteID { get; set; }

        public string Cliente { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataCadastro { get; set; }

        public string Fase { get; set; }

        public string Realizador { get; set; }

        public string Descricao { get; set; }

        public bool Concluida { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataRealizacao { get; set; }

        public string OportunidadeID { get; set; }

        public int CodigoOportunidade { get; set; }

        public string CriadoPorID { get; set; }

        public string CriadoPor { get; set; }

        public string AlteradoPorID { get; set; }

        public string AlteradoPor { get; set; }
    }
}