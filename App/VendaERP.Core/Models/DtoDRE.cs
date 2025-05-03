using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoDRE
    {
        public string Empresa { get; set; }
        public string PlanoDeConta { get; set; }
        public string Grupo { get; set; }
        public int AnoInicial { get; set; }
        public double AnoInicialValor { get; set; }
        public int AnoFinal { get; set; }
        public double AnoFinalValor { get; set; }
    }

    [Serializable]
    public class DtoDREPeriodo
    {
        public string Empresa { get; set; }
        public string PlanoDeConta { get; set; }
        public string Grupo { get; set; }
        public string PeriodoInicial { get; set; }
        public double PeriodoInicialValor { get; set; }
        public string PeriodoFinal { get; set; }
        public double PeriodoFinalValor { get; set; }
        public double TotalFinal { get; set; }
        public double TotalInicial { get; set; }
    }

    [Serializable]
    public class DtoDREContabil
    {
      //  public string Empresa { get; set; }
        public double ReceitaOperacionalBruta { get; set; }
        public double DeducoesAbatimentos { get; set; }
        public double ReceitaOperacionalLiquida { get; set; }
        public double CMV { get; set; }
        public double CPV { get; set; }
        public double CSV { get; set; }
        public double ResultadoOperacionalBruto { get; set; }
        public double DespesasOperacionais { get; set; }
    }
}
