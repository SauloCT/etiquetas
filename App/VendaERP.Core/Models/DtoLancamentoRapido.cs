using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLancamentoRapido
    {
        public string PlanoDeConta { get; set; }
        public string Cliente { get; set; }
        public string ClienteID { get; set; }
        public bool Despesa { get; set; }
        public bool Valido { get; set; }
        public DateTime Vencimento { get; set; }
        public double Valor { get; set; }
    }
}