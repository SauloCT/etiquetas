


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaCategoria : EntityLastUpdate
    {
        [KeyAttribute("VendaCategoria")]

        public string Nome { get; set; }


        public bool EhOperacaoFiscal { get; set; }


        public bool CanChange { get; set; }


        public bool EhOperacaoPadrao { get; set; }


        public bool? MovimentaEstoque { get; set; }


        public bool? MovimentaFinanceiro { get; set; }
    }
}
