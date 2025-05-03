using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducaoCheckList : Entity
    {
        public string OrdemProducaoID { get; set; }

        public int NivelPrioridade { get; set; }

        public int NivelComplexidade { get; set; }

        public string Descricao { get; set; }

        public int Ordenacao { get; set; }

        public bool Validado { get; set; }

        public string Observacoes { get; set; }
    }
}