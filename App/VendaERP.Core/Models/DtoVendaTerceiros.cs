using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVendaTerceiros : Entity
    {
        public int Codigo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        public string Empresa { get; set; }

        public string Representado { get; set; }

        public string Cliente { get; set; }

        public string Vendedor { get; set; }

        public double ValorVenda { get; set; }

        public double ValorComissao { get; set; }

        public double PercentualComissao { get; set; }

        public double ValorComissaoVendedor { get; set; }

        public double PercentualComissaoVendedor { get; set; }

        public string Descricao { get; set; }

        public int NumeroParcelas { get; set; }

        public string PeriodoParcelamento { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVencimento { get; set; }

        public bool Lancado { get; set; }

        public bool LancadoVendedor { get; set; }

    }
}
