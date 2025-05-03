
using System.Collections.Generic;


namespace VendaERP.Core.Models
{
    public class DtoEstoqueCotacao : Entity
    {
        public DtoEstoqueCotacao()
        {
            this.ProdutoIds = new List<string>();
        }

        public List<string> ProdutoIds { get; set; }
    }
}
