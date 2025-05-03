

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoPlanoDeConta : EntityLastUpdate
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataAlteracao { get; set; }

        public string CodigoNatureza { get; set; }

        public string TipoDeConta { get; set; }



        public string Nome { get; set; }


        public bool EhDespesa { get; set; }


        public string Hierarquia { get; set; }


        public string CodigoGrupo { get; set; }


        public string GrupoDRE { get; set; }

        public string GrupoDREID { get; set; }

        public string PlanoPaiId { get; set; }

        public int OrdemNivel { get; set; }

        public bool DesativarPlano { get; set; }

    }
}