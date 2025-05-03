using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoPessoaReferencia
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Contato { get; set; }

        public double ValorMedio { get; set; }
    }
}
