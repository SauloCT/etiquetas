using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLicencasSistema
    {
        public Guid Codigo { get; set; }

        public string Nome { get; set; }
    }
}