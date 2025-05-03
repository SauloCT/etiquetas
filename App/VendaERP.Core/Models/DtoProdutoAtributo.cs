
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    public class DtoProdutoAtributo : Entity
    {
        public DtoProdutoAtributo()
        {
            
        }


        public string Nome { get; set; }


        public string Abreviacao { get; set; }

        public string[] Valores { get; set; }

        public int AtributoIdOnMagento { get; set; }

        public string NomeOnMagento { get; set; }

        

    }
}
