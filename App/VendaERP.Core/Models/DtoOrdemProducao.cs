
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducao : Entity
    {
        public DtoOrdemProducao() => Products = new List<ProductionOrderProduct>();

        [AutoIncrement]
        public int Codigo { get; set; }

        public string EmpresaID { get; set; }


        public string Empresa { get; set; }

        public string DepositoID { get; set; }

        public string Deposito { get; set; }


        public string Cliente { get; set; }

        public string ClienteID { get; set; }

        public string PedidoID { get; set; }


        public long CodigoPedido { get; set; }


        public string Situacao { get; set; }


        public string ResponsavelProducao { get; set; }


        public string ResponsavelQualidade { get; set; }

        public bool EhResponsavelFinalizacao { get; set; }

        public string ResponsavelFinalizacao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataInicio { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataTermino { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PrevisaoInicio { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PrevisaoTermino { get; set; }

        public string Observacoes { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime QA_DataAvaliacao { get; set; }


        public int QA_NivelQualidade { get; set; }


        public string QA_Plano { get; set; }

        public string QA_Avaliacao { get; set; }

        public string QA_Observacoes { get; set; }


        public string Status { get; set; }

        public bool ManterPrevisao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }


        public bool OPImpressa { get; set; }

        public bool QAImpressa { get; set; }


        public List<ProductionOrderProduct> Products { get; set; }

        public bool CriadoComposicaoSistema { get; set; }


        public string Lote { get; set; }
    }

    public class ProductionOrderProduct
    {
        public ProductionOrderProduct()
        {
            Attributes = new List<AtributoMovimentacao>();
            Series = new List<string>();
        }

        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountProduced { get; set; }
        public decimal AmountDiscarded { get; set; }
        public string MeasureUnit { get; set; }
        public bool MeasureUnitHasSerie { get; set; }
        public string GridId { get; set; }
        public string Grid { get; set; }
        public string Lot { get; set; }
        public List<AtributoMovimentacao> Attributes { get; set; }
        public List<string> Series { get; set; }
        public int Index { get; set; }
    }
}