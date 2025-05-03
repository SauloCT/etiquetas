using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoSimilar : Entity, ICloneable
    {
        public string ProdutoID { get; set; }        

        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }

        public string Fabricante { get; set; }

        public bool? MigradoParaDtoProduto { get; set; }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public DtoProdutoSimilar Clone()
        {
            var clone = new DtoProdutoSimilar
            {
                ProdutoID = ProdutoID,
                Codigo = Codigo,
                Nome = Nome,
                Modelo = Modelo,
                Marca = Marca,
                Fabricante = Fabricante
            };

            return clone;
        }
    }
}
