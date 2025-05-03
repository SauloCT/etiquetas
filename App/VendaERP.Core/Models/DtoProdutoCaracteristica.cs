


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoCaracteristica : EntityLastUpdate
    {
        public string Nome { get; set; }
    }

    [Serializable]
    public class CaracteristicaValor
    {
        public CaracteristicaValor()
        {
        }

        public string Id { get; set; }

        //Sempre igual ao Nome do DtoProdutoCaracteristica
        public string Nome { get; set; }

        public string Valor { get; set; }
    }
}
