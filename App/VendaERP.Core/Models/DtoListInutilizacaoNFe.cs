using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoListInutilizacaoNFe
    {
        public string codigo { get; set; }

        public string Empresa { get; set; }

        public DateTime dataInutilizacao { get; set; }

        public double numeracaoInicial { get; set; }

        public double numeracaoFinal { get; set; }
    }
}