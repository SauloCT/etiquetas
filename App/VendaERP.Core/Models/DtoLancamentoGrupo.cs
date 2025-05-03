


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLancamentoGrupo : Entity
    {
        [KeyAttribute("LancamentoGrupo")]

        public string Nome { get; set; }


        public string Observacoes { get; set; }
    }
}