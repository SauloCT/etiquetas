using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProcessoProdutivo : Entity
    {
        public string Nome { get; set; }

        public bool Tercerizado { get; set; }

        public bool MovimentaConsumoQuandoFinalizado { get; set; }

        public bool MovimentaConsumoQuandoInicializado { get; set; }

        public string Observacoes { get; set; }

    }
}