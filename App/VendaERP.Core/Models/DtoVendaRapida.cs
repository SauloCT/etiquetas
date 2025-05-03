using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaRapida : Entity
    {
        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public string Empresa { get; set; }

        public bool Lancado { get; set; }

        [BsonIgnore]
        public bool Valido { get; set; }

        public string ClienteID { get; set; }

        public string ProdutoID { get; set; }

        public string Cliente { get; set; }

        public string CodigoProduto { get; set; }

        public string Produto { get; set; }

        public double PrecoUnitario { get; set; }

        public double Quantidade { get; set; }

        public double ValorTotal { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVenda { get; set; }

        public string NumeroNFe { get; set; }

        public bool GerarDespesa { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Vencimento { get; set; }
    }
}