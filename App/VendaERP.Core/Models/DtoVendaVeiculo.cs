using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaVeiculo : Entity
    {
        public string Modelo { get; set; }

        public string Placa { get; set; }

        public string Nserie { get; set; }

        public string Chassis { get; set; }

        public string OSID { get; set; }

        public string VendaID { get; set; }
    }
}
