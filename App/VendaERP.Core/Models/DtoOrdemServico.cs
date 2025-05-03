using System;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemServico : Entity
    {
        [AutoIncrement]
        public long Codigo { get; set; }

        public string VendaID { get; set; }

        public string EmpresaID { get; set; }

        public string Empresa { get; set; }

        public string ClienteID { get; set; }

        public string Cliente { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AberturaOrdemServico { get; set; }

        public string Status { get; set; }

        public string StatusSistema { get; set; }

        public string TecnicoID { get; set; }

        public string Tecnico { get; set; }

        public string Contrato { get; set; }

        public string Contato { get; set; }

        public string EmailResponsavel { get; set; }

        public string EmailFaturamento { get; set; }

        public string EmailComercial { get; set; }

        public string Problema { get; set; }

        public string Laudo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVisita { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IntervencaoTecnicaInicio { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IntervencaoTecnicaFim { get; set; }

        public string NumeroNFSe { get; set; }

        public string MotivoCancelamentoOS { get; set; }

        public string Pais { get; set; }

        public string CEP { get; set; }

        public string UF { get; set; }

        public string UFCodigo { get; set; }

        public string CodigoMunicipio { get; set; }

        public string Municipio { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string LogradouroNumero { get; set; }

        public string LogradouroComplemento { get; set; }

        public bool OcultarCampos { get; set; }

    }
}