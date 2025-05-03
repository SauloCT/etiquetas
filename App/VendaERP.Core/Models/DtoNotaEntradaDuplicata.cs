using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoNotaEntradaDuplicata : Entity
    {
        public string EntradaID { get; set; }

        public string VendaID { get; set; }

        public string ContratoID { get; set; }

        public double Codigo { get; set; }

        public string NumeroDuplicata { get; set; }

        public DateTime DataVencimentoDuplicata { get; set; }

        public double ValorDuplicata { get; set; }

        public bool Adiantamento { get; set; }

        public bool MigradaParaPagamentoVenda { get; set; }
    }
}