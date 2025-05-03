

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoSped : Entity
    {
        public string EmpresaID { get; set; }


        public string Empresa { get; set; }


        public DateTime DataInicial { get; set; }


        public DateTime DataFinal { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataGeracao { get; set; }

        public DateTime DataVencimentoGuiaICMS { get; set; }


        public string UserName { get; set; }


        public string Perfil { get; set; }


        public bool Retificacao { get; set; }

        public string UserId { get; set; }

        public string Arquivo { get; set; }


        public string NomeArquivo { get; set; }


		public string CodigoICMS { get; set; }



	}
}