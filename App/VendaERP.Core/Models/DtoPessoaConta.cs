using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoPessoaConta
    {
        public string NumeroConta { get; set; }

        public string Agencia { get; set; }

        public string Banco { get; set; }
    }
}
