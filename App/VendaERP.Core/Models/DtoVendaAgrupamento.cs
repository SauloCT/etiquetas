


using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoVendaAgrupamento : EntityLastUpdate
	{
		public DtoVendaAgrupamento()
		{
			this.Pedidos = new List<ItemVendaAgrupamento>();
		}


		public string Cliente { get; set; }
		public string ClienteID { get; set; }

		public string Empresa { get; set; }
		public string EmpresaID { get; set; }

		[AutoIncrement]

		public long Codigo { get; set; }


		public IEnumerable<long> CodigosPedidosAdicionados
		{
			get
			{
				if (this.Pedidos != null && this.Pedidos.Count() > 0)
					return this.Pedidos.Select(q => q.Codigo);

				return new List<long>();
			}
			set
			{

			}
		}


		public IEnumerable<ItemVendaAgrupamento> Pedidos { get; set; }
	}

	public class ItemVendaAgrupamento
	{
		public string VendaID { get; set; }
		public DateTime Data { get; set; }
		public long Codigo { get; set; }
		public string Cliente { get; set; }
		public string ClienteID { get; set; }
		public string Empresa { get; set; }
		public string EmpresaID { get; set; }
		public bool EhOrdemServico { get; set; }
		public IEnumerable<DtoVendaProduto> ItensVenda { get; set; }
		public string Tecnico { get; set; }
		public string TecnicoID { get; set; }
		public double Valor { get; set; }
		public double VendaFrete { get; set; }
		public double VendaSeguro { get; set; }
		public double VendaOutrasDespesas { get; set; }
		public double VendaDesconto { get; set; }
		public double VendaTotalSemDesconto { get; set; }
		public double VendaValorFinal { get; set; }
		public string StatusSistema { get; set; }
		public string StatusCliente { get; set; }
		public string StatusClienteID { get; set; }
		public IEnumerable<DtoVendaEquipamento> Equipamentos { get; set; }
		public IEnumerable<DtoVendaLaudo> Laudos { get; set; }
		public IEnumerable<DtoVendaVeiculo> Veiculos { get; set; }
	}
}