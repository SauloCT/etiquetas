



using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoCategoriaProduto : Entity
	{
		public DtoCategoriaProduto()
		{
		}

		[IndexedColumn()]
		[Key("CategoriaProdutoPai")]

        public string Nome { get; set; }

		public string NomeWeb { get; set; }

		public string Titulo { get; set; }

		public string PalavrasChave { get; set; }

		public string Descricao { get; set; }

		public bool Destaque { get; set; }

		[IndexedColumn()]

        public string Hierarquia { get; set; }

		public string FiltrosCategoria { get; set; }

		/// <summary>
		/// Id da categoria no Magento se sincronizado
		/// </summary>
		public int MagentoCategoriaId { get; set; }

		/// <summary>
		/// Indicador se precisa de sincronização com o Magento por alterações efetuadas no SIGE
		/// </summary>
		public bool MagentoNeedUpdate { get; set; }

		
	}
}