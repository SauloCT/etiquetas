


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoGrupoDRE : Entity
    {

        public string Codigo { get; set; }

        [KeyAttribute("GrupoDRE")]

        public string Nome { get; set; }


        public string Hierarquia { get; set; }
    }
}