using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLinhaProducao : Entity
    {
        public int NumeroLinha { get; set; }

        public string Nome { get; set; }

        public bool LinhaAtiva { get; set; }

        public bool UsoObrigatorioFerramenta { get; set; }

        public bool LinhaTercerizada { get; set; }

        public string Pessoa { get; set; }
    }
}