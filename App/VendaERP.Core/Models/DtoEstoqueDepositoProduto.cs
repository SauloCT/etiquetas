using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueDepositoProduto : EntityLastUpdate
    {
        public string Produto { get; set; }

        [IndexedColumn()]
        public string ProdutoID { get; set; }

        public string Deposito { get; set; }

        [IndexedColumn()]
        public string DepositoID { get; set; }

        public double CompraMaxima { get; set; }

        public double EstoqueMinimo { get; set; }

        public double Saldo { get; set; }

        public List<AtributosProdutoSaldo> SaldosAtributos { get; set; }

        public List<string> NumerosSerie { get; set; }

        [BsonIgnore]
        public double AuxMovExcel { get; set; }

        [BsonIgnore]
        public List<Lotes> Lotes { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UltimaAtualizacao { get; set; }
    }

    public class Lotes
    {
        public string LoteID { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
		public DateTime Fabricacao { get; set; }
		public double Saldo { get; set; }
    }

    public class AtributosProdutoSaldo
    {
        public List<AtributoMovimentacao> Atributos { get; set; }
        public double Saldo { get; set; }
    }

    [Flags]
    public enum TipoMovimentacaoEstoque
    {
        Compra = 0,
        Venda = 1,
        Interno = 2,
        Devolucao = 3,
        Cancelamento = 4,
        Transferencia = 5,
        EntradaNotaFiscal = 6,
        Producao = 7,
        EntreEstoques = 8,
        EntradaPlanilhaExcel = 9,
        SaidaPlanilhaExcel = 10,
        SaidaVendaConsignada = 11,
        EntradaVendaConsignada = 12,
        EntradaProdutoOrigemMagento = 13,
        EntradaVendaMagento = 14,
        EntradaProdutoOrigemWooCommerce = 15,
        EntradaLite = 16,
        SaidaLite = 17
    }
}