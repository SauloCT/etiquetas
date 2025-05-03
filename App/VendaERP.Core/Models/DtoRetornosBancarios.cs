
using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoRetornosBancarios : EntityLastUpdate
    {
        public string EmpresaNome { get; set; }
        public List<RetornoBancario> ListaRetornosBancarios { get; set; }
    }
    public class RetornoBancario
    {
        public string Duplicata { get; set; }
        public string Cliente { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorJurosMulta { get; set; }
        public int Comando { get; set; }
        public string Observacao { get; set; }
        public decimal ValorPago { get; set; }
        public int CodigoBanco { get; set; }
        public string NumeroBancario { get; set; }
        public decimal Despesa { get; set; }
    }
}