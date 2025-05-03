using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueReservaProduto : Entity
    {
        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public string ProdutoID { get; set; }

        public string ReservaID { get; set; }

        public double Codigo { get; set; }

        public string CodigoNFE { get; set; }

        public string Unidade { get; set; }

        public string Descricao { get; set; }

        public string Marca { get; set; }

        public double Quantidade { get; set; }

        public double ValorUnitario { get; set; }

        [BsonIgnore]
        public double ValorUnitarioSemComissao { get; set; }

        public double ComissaoUnitario { get; set; }

        public double ComissaoUnitarioPercentual { get; set; }

        public double ValorTotal { get; set; }

        public double ValorCustoUnitario { get; set; }

        public double ValorCustoTotal { get; set; }

        public double ComissaoTotal { get; set; }

        public double ComissaoUnitariaRepresentacao { get; set; }

        public double ComissaoUnitariaRepresentacaoPercentual { get; set; }

        public double ComissaoTotalRepresentacao { get; set; }

        public double DescontoUnitario { get; set; }

        public double DescontoUnitarioPercentual { get; set; }

        public double DescontoTotal { get; set; }

        /// <summary>
        /// Prazo de Entrega 
        /// </summary>
        public int PrazoEntregaFrete { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        [BsonIgnore]
        public bool IgnorarAlertaEstoque { get; set; }

        [BsonIgnore]
        public double EstoqueSaldo { get; set; }

        [BsonIgnore]
        public double CompraMaxima { get; set; }

        public string KitProdutoPaiID { get; set; }

    }

}