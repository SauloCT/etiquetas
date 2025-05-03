using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoQualidade : Entity
    {        
        public string Produto { get; set; }

        public string ProdutoID { get; set; }

        public int NivelPrioridade { get; set; }

        public int NivelComplexidade { get; set; }

        public string Descricao { get; set; }

        public int Ordenacao { get; set; }
    }
}