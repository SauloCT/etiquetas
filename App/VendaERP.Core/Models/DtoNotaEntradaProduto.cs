using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoNotaEntradaProduto : Entity
    {
        public string EntradaID { get; set; }

        public string ProdutoID { get; set; }

        public double Codigo { get; set; }

        public string CodigoNFe { get; set; }

        public string Descricao { get; set; }

        public double Quantidade { get; set; }

        public string ProdutoUnidade { get; set; }

        public double ValorUnitario { get; set; }

        public double Desconto { get; set; }

        public double TotalUnitario { get; set; }

        public double OrigemMercadoria_NFe { get; set; }

        public string CodigoCEST { get; set; }

        public double ValorTotal { get; set; }

        public bool SemLote { get; set; }

        public string Lote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime FabricacaoLote { get; set; }
		public bool PossuiRastreabilidade { get; set; }

		/// <summary>
		/// Preencher este valor apenas se o XML da nota de entrada possuir a informação. Caso contrário, o SIGE trabalha com 1 informação de lote para cada produto
		/// </summary>
		public List<DadosLote> Lotes { get; set; }

        public bool EhServico { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public List<string> ChaveAcesso { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        public string CodigoDescricaoXML { get; set; }

        #region Fiscal

        public string NCM { get; set; }

        public string CodigoCFOP { get; set; }

        public double ValorFrete { get; set; }

        public double Seguro { get; set; }

        #region IPI

        public double IPI_BaseCalculo { get; set; }

        public double IPI_Valor { get; set; }

        #endregion

        #region ICMS

        public double ICMS_BaseDeCalculo { get; set; }

        public double ICMS_Valor { get; set; }

        public double ICMS_STBaseCalculoValor { get; set; }

        public double ICMS_STValor { get; set; }

        #endregion

        #region PIS

        public double PIS_BaseCalculo { get; set; }

        public double PIS_Valor { get; set; }

        #endregion

        #region COFINS

        public double COFINS_BaseCalculo { get; set; }

        public double COFINS_Valor { get; set; }

        #endregion

        #region ISS

        public double ISS_BaseCalculo { get; set; }

        public double ISS_Valor { get; set; }

        #endregion

        #endregion

        public double QuantidadePorPacote { get; set; }

        public double QuantidadeConvertidaEmUnidade { get; set; }

        [BsonIgnore]
        public string Prateleira { get; set; }
    }

    [Serializable]
    public class DadosLote
    {
        public string Lote { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public double Quantidade { get; set; }
    }
}