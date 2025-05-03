
using System;


namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoMarca : EntityLastUpdate
	{
		public DtoMarca()
		{
			
		}


		public string Nome { get; set; }

	
	}
}