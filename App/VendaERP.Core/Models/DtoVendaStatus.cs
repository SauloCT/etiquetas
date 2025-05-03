


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaStatus : Entity
    {
        [KeyAttribute("VendaStatus")]

        public string Nome { get; set; }

        public string CategoriaID { get; set; }


        public string Categoria { get; set; }


        public int Ordenacao { get; set; }
    }
}
