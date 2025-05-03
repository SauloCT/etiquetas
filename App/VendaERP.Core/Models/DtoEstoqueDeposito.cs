using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;





namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueDeposito : EntityLastUpdate
    {
        

        public string Nome { get; set; }

        public string EmpresaID { get; set; }

        

        public string Empresa { get; set; }

        public string Logradouro { get; set; }

        public string LogradouroNumero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string CodigoMunicipio { get; set; }

        public string Pais { get; set; }

        public string CodigoPais { get; set; }


        public string CEP { get; set; }


        public string UF { get; set; }

        public string CodigoUF { get; set; }

        public string Responsavel { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public bool EnderecoExterior { get; set; }
    }
}