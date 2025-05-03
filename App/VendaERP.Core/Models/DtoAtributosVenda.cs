using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace VendaERP.Core.Models
{
    public class DtoAtributosVenda : Entity
    {
        public string Nome { get; set; }

        public string Model { get; set; }

        public string Tipo { get; set; }

        public string AtributoID { get; set; }
    }
}
