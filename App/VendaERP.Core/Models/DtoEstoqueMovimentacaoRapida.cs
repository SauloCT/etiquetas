using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueMovimentacaoRapida
    {
        public string DepositoID { get; set; }   
        
        public string ProdutoID { get; set; }
        
        public string Deposito { get; set; }

        public double EstoqueSaldo { get; set; }
        
        public string Empresa { get; set; }
        
        public string CodigoProduto { get; set; }
        
        public string Produto { get; set; }
        
        public double Quantidade { get; set; }
        
        public string Unidade { get; set; }
        
        public double CustoUnitario { get; set; }
        
        public double CustoTotal { get; set; }
        
        public DateTime Data { get; set; }

        public bool Valido { get; set; }
    }
}