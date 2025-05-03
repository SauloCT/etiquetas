using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLancamentoVenda
    {
        public string ClienteNomeFantasia { get; set; }

        public string ClienteCNPJ_CPF { get; set; }

        public string ClienteLogradouro { get; set; }

        public string ClienteCidade { get; set; }

        public string ClienteUF { get; set; }

        public string EmpresaNomeFantasia { get; set; }

        public string EmpresaCNPJ { get; set; }

        public string EmpresaEmail { get; set; }

        public string EmpresaTelefone { get; set; }

        public string EmpresaLogradouro { get; set; }

        public string EmpresaCidade { get; set; }

        public string EmpresaUF { get; set; }

        public string EmpresaID { get; set; }


        public long VendaCodigo { get; set; }

        public int CodigoLancamento { get; set; }

        public double ValorLancamento { get; set; }

        public DateTime DataVencimentoLancamento { get; set; }

        public string Descricao { get; set; }

    }
}