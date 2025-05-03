using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemCompraitem : Entity
    {
        public string OrdemCompraID { get; set; }

        public string ProdutoID { get; set; }

        public string CodFornecedor { get; set; }

        public string Produto { get; set; }

        public string CodigoNFe { get; set; }

        public string Marca { get; set; }

        public string Unidade { get; set; }

        public double Quantidade { get; set; }

        public double ValorUnitario { get; set; }

        public double ValorFrete { get; set; }

        public double ValorSemFrete { get; set; }

        public double ValorDespesas { get; set; }

        public double ValorTotal { get; set; }

        public double DescontoUnitario { get; set; }

        public double DescontoUnitarioPercentual { get; set; }

        public double UltimoPreco { get; set; }

        public string CotacaoItemCotadoID { get; set; }

        #region Impostos
        public double ICMS_ST_MVA { get; set; }

        public double ICMS_ST_Aliquota { get; set; }

        public double ICMS_ST_Aliquota_Percentual { get; set; }

        public double ICMS_ST_Valor { get; set; }

        public double ICMS_Aliquota { get; set; }

        public double IPI_Aliquota { get; set; }

        public double ImpostoImportacao_Aliquota { get; set; }
        #endregion

        #region Solicitacao
        public bool Urgente { get; set; }

        public string Solicitante { get; set; }

        public DateTime DataSolicitacao { get; set; }
        #endregion

        public string Descricao { get; set; }

        [BsonIgnore]
        public DtoProdutoSimilar[] ProdutoSimilar { get; set; }
    }
}