
using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoImagemProduto : EntityLastUpdate
    {
        public DtoImagemProduto()
        {
            
        }

        [IndexedColumn()]
        public string ProdutoID { get; set; }

        
        public string ImagemDataAWSUrl { get; set; }

        public string ImagemMiniaturaAWSUrl { get; set; }

        public int ImagemSize { get; set; }

        public string Tipo { get; set; }

        public bool Principal { get; set; }

        [IndexedColumn()]
        public string ECommerceImageId { get; set; }

        public string FileNameOnMagento { get; set; }

        
    }
}