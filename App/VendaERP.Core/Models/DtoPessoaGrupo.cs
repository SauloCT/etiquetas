


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoPessoaGrupo : Entity
    {
        [KeyAttribute("PessoaGrupo")]

        public string Nome { get; set; }


        public string Observacoes { get; set; }
    }
}