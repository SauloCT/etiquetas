
using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducaoQualidade
    {
        public string OrdemProducaoID { get; set; }


        public int Codigo { get; set; }


        public string Situacao { get; set; }


        public string Empresa { get; set; }


        public string ResponsavelQualidade { get; set; }


        public DateTime QA_DataAvaliacao { get; set; }


        public int QA_NivelQualidade { get; set; }



        public string QA_Plano { get; set; }


        public string QA_Avaliacao { get; set; }


        public string QA_Observacoes { get; set; }



        public DateTime DataInicio { get; set; }


        public DateTime DataTermino { get; set; }


        public DateTime PrevisaoInicio { get; set; }


        public DateTime PrevisaoTermino { get; set; }
    }
}