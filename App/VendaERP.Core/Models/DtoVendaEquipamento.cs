using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaEquipamento : Entity
    {
        public string Codigo { get; set; }

        public string OSID { get; set; }

        public string VendaID { get; set; }

        public string Nome { get; set; }

        public string Observacoes { get; set; }

        public string FeedBack { get; set; }

        public string Modelo { get; set; }

        public string Fabricante { get; set; }
    }
}