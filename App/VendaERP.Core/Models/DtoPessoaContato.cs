using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoPessoaContato : Entity
    {
        
        public string PessoaID { get; set; }


        public string Nome { get; set; }

  
        public string NomeEmpresa { get; set; }

      
        public string Empresa { get; set; }

        public string EmpresaID { get; set; }

        public string EmpresaCNPJ { get; set; }


        public string Departamento { get; set; }


        public string Cargo { get; set; }


        public string Telefone { get; set; }

        public string Ramal { get; set; }

        public string Celular { get; set; }

        public string Fax { get; set; }


        public string Email { get; set; }

        public string SkypeMSN { get; set; }
    }
}