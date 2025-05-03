

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoProdutoComposicao : Entity
	{
		public string DepositoID { get; set; }

		public string Deposito { get; set; }

		public string Produto { get; set; }        

		public string ProdutoID { get; set; }

		[IndexedColumn()]
		public string ProdutoPaiID { get; set; }

		public string Genero { get; set; }

		public double Quantidade { get; set; }

		public double OutrosCustos { get; set; }

		public double PrecoCompra { get; set; }

		public double CustoUnitario { get; set; }

		public double CustoTotal { get; set; }

		public string Codigo { get; set; }

		public string EstoqueUnidade { get; set; }

		public string Observacoes { get; set; }

		public bool IgnorarEstoque { get; set; }

		public string GradeID { get; set; }

		public string Grade { get; set; }

		public List<AtributoMovimentacao> AtributosProduto { get; set; }

		public bool IncidirImpostosNoCusto { get; set; }

		public double PrecoVendaProduto { get; set; }

		public double Desconto { get; set; }

		public double PrecoVenda { get; set; }

		public double PrecoVendaFinal { get; set; }
	}
}