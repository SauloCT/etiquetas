


using System;

namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoFormaPagamento : EntityLastUpdate
	{
  
        public string Nome { get; set; }


        public bool IgnorarLimiteCredito { get; set; }


		public bool DesativarFormaPagamento { get; set; }
	}
}