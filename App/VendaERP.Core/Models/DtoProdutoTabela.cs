

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoTabela : EntityLastUpdate
    {

        public string Nome { get; set; }


        public string Genero { get; set; }



        public bool ExibirNoPDV { get; set; }

        public bool AtualizarPrecoVenda { get; set; }
    }
}