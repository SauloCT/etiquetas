using System;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueReserva : Entity
    {
        [AutoIncrement]
        public int Codigo { get; set; }

        public string Cliente { get; set; }

        public string ClienteID { get; set; }

        public string Status { get; set; }

        public string TabelaDePreco { get; set; }

        public string TabelaDePrecoID { get; set; }

        public string Empresa { get; set; }

        public string EmpresaID { get; set; }

        public string Deposito { get; set; }

        public string DepositoID { get; set; }

        public string Vendedor { get; set; }

        public string VendedorID { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataReserva { get; set; }

        public int DiasReserva { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataTerminoReserva { get; set; }

        public string Observacoes { get; set; }

        public string ClienteRG { get; set; }

        public string ClienteCpfCnpj { get; set; }

        public string ClienteEmail { get; set; }

        public string ClienteTelefone { get; set; }

        public string ClienteLogradouro { get; set; }

        public string ClienteLogradouroNumero { get; set; }

        public string ClienteComplemento { get; set; }

        public string ClienteBairro { get; set; }

        public string ClienteCEP { get; set; }

        public string ClienteCidade { get; set; }

        public string ClienteCodigoMunicipio { get; set; }

        public string ClienteUF { get; set; }

        public string ClienteCodigoUF { get; set; }

        public string ClientePais { get; set; }

        public string ClienteCodigoPais { get; set; }

    }
}
