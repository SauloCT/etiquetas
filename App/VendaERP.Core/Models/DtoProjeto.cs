using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProjeto : Entity
    {
        public String Nome { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataTermino { get; set; }
    }
}
