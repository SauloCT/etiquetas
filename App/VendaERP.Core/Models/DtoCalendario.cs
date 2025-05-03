using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCalendario
    {
        public DateTime Data { get; set; } 
        public double Receita { get; set; }
        public double Despesa { get; set; }
        public double Saldo { get; set; }
        //public bool PossuiNota { get; set; }
        //public bool PossuiPendencias { get; set; }
    }
}