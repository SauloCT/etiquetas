


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoCatalogo : Entity
    {

        public string Nome { get; set; }

        public string EmpresaID { get; set; }


        public string Empresa { get; set; }


        public string TabelaPreco { get; set; }

        public string TabelaPrecoID { get; set; }


        public bool OcultarPreco { get; set; }


        public string Categoria { get; set; }

        public string CategoriaID { get; set; }

    }
}
