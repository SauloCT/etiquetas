using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoSaldo
    {
        public double Receita { get; set; }
        public double Despesa { get; set; }
        public double Saldo { get; set; }

        public double PrevisaoReceita { get; set; }
        public double PrevisaoDespesa { get; set; }
        public double PrevisaoSaldo { get; set; }

        public double LanVencidosReceita { get; set; }
        public double LanVencidosDespesa { get; set; }
        public double LanVencidosSaldo { get; set; }

        public DateTime DataConsulta { get; set; }
    }
}