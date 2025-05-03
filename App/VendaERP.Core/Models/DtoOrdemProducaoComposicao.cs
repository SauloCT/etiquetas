using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducaoComposicao : Entity
    {

        public string OrdemProducaoID { get; set; }

        public string ProdutoPaiID { get; set; }

        public string ProdutoID { get; set; }

        public string Produto { get; set; }

        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public string Genero { get; set; }

        public string EstoqueUnidade { get; set; }

        public double CustoUnitario { get; set; }

        public double CustoTotal { get; set; }

        public double Quantidade { get; set; }

        public string LoteID { get; set; }

        public string Lote { get; set; }

        public List<AtributosLote> Lotes { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

        public double EstoqueAtual { get; set; }

        public string Observacoes { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public List<DtoProdutoComposicao> SubItens { get; set; }

        /// <summary>
        /// foi adicionado esse campo para mostrar na listagem os composto no produto certo, quando existe o mesmo produto na OP
        /// </summary>
        public int Index { get; set; }
    }

    public class AtributosLote
    {
        public string LoteID { get; set; }
        public string Lote { get; set; }
        public int QuantidadeLote{ get; set; }
        public double Saldo{ get; set; }
        public DateTime Fabricacao{ get; set; }
        public DateTime Validade{ get; set; }
    }
}