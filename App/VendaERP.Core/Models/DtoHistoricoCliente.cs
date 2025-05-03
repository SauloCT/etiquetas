using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoHistoricoCliente : Entity
    {
        public Guid Codigo { get; set; }

        public int CodigoOp { get; set; }

        public DateTime Data { get; set; }

        public string Responsavel { get; set; }

        public string Origem { get; set; }

        public string Fase { get; set; }

        public string Cliente { get; set; }

        public string Fone { get; set; }

        public DateTime DataRealizacao { get; set; }

        public string EventosPendentes { get; set; }


    }
}