


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoAtributos : Entity
    {

        public string Nome { get; set; }

        public string Model { get; set; }


        public string Tipo { get; set; }

    }

    public enum EnumAtributoTipo
    {
        PRODUTO = 1,
        VENDA = 2
    }
}
