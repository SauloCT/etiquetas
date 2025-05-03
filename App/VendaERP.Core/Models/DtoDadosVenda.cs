using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoDadosVenda
    {

        public string ClienteNomeFantasia { get; set; }

        public string ClienteCNPJ_CPF { get; set; }

        public string ClienteLogradouro { get; set; }

        public string ClienteCidade { get; set; }

        public string ClienteUF { get; set; }


        public string EmpresaID { get; set; }

        public string EmpresaNomeFantasia { get; set; }

        public string EmpresaRazaoSocial { get; set; }

        public string EmpresaCNPJ { get; set; }

        public string EmpresaEmail { get; set; }

        public string EmpresaTelefone { get; set; }

        public string EmpresaLogradouro { get; set; }

        public string EmpresaCidade { get; set; }

        public string EmpresaUF { get; set; }

        public Guid VendaID { get; set; }

        public long VendaCodigo { get; set; }

        public DateTime VendaData { get; set; }

        public string VendaTransportadora { get; set; }

        public string VendaFreteEmitente { get; set; }

        public double VendaDesconto { get; set; }

        public double VendaValorFrete { get; set; }

        public double VendaOutrasDesepsas { get; set; }

        public double VendaValor { get; set; }

        public double VendaComissao { get; set; }

        public Guid VendaVendedorID { get; set; }

        public string VendaVendedorNome { get; set; }


        public string ComissaoPaga { get; set; }

        public double comissoesReceber { get; set; }

        public double comissoesRecebidas { get; set; }

        public string Descricao { get; set; }

        public bool Pedido { get; set; }



    }
}