
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoOpcoesAlimenticio : EntityLastUpdate
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool Pause { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public ProductFoodOptionType Type { get; set; }

        public ProductFoodGroup Group { get; set; }

        public List<ProductFoodOptionsPrice> Prices { get; set; }
    }

    public class ProductFoodOptionsPrice
    {
        public string ProductVariationId { get; set; }

        public double Price { get; set; }
    }

    public class ProductFoodGroup
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ProductFoodOptionType Type { get; set; }
    }

    public enum ProductFoodOptionType
    {
        Sabor,
        Complemento
    }


    
}
