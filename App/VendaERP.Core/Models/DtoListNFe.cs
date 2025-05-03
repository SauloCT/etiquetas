using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoListNFe
    {
        public string Codigo { get; set; }

        public int Numero { get; set; }

        public string Serie { get; set; }

        public int Lote { get; set; }

        public DateTime DataEmissao { get; set; }

        public string IDNfe { get; set; }

        public string Status { get; set; }

        public DateTime DataStatus { get; set; }

        public string Cliente { get; set; }

        public double Valor { get; set; }

        public string DataRecebimentoEmail { get; set; }

    }
}