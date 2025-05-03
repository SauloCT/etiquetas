


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoListEventosNFe : Entity
    {

        public string OrigemEvento { get; set; }

        public string Codigo { get; set; }


        public int CodigoSequencial { get; set; }


        public string TipoEvento { get; set; }


        public DateTime DataEvento { get; set; }


        public string SerieNFe { get; set; }


        public int NumeroNFe { get; set; }


        public string NFeID { get; set; }

        /// <summary>
        /// Utilizado nas Nfse, para buscar a nota...
        /// </summary>
        public string NotaFiscalId { get; set; }
    }
}