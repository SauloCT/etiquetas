


using System;

namespace VendaERP.Core.Models
{
    public class DtoLogsOrdemServico : EntityLastUpdate
    {

        public long CodigoOS { get; set; }


        public string Usuario { get; set; }


        public string StatusAnterior { get; set; }


        public string NovoStatus { get; set; }


        public string Tipo { get; set; }



        public DateTime DataModificacao { get; set; }
    }
}
