using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoRelatorioEstoque
    {
        public string Nome { get; set; }
        public string Deposito { get; set; }
        public string Marca { get; set; }
        public double Codigo { get; set; }
        public string Especificacao { get; set; }
        public double precoCusto { get; set; }
        public double precoVenda { get; set; }
        public double Estoquesaldo { get; set; }
        public string Fornecedor { get; set; }
        public double EstoqueMinimo { get; set; }
    }
}