using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCredenciadoraTaxasParcelas
    {
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public string PeriodoRecebimento { get; set; }
    }
}
