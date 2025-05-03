
using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCotacao : Entity
    {

        [AutoIncrement]
        public int Codigo { get; set; }
        public string Aprovador { get; set; }
        public string AprovadorID { get; set; }
        public string EmpresaID { get; set; }

        public string Empresa { get; set; }
        public string DepositoID { get; set; }
        public string Deposito { get; set; }


        [IndexedColumn]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PrazoResposta { get; set; }


        public StatusCotacao Status { get; set; }


        public StatusAprovacao StatusAprovacao { get; set; }

        public string FormadePagamentoID { get; set; }

        public string FormadePagamento { get; set; }

        public string ArquivoAnexadoID { get; set; }

        public string ArquivoAnexado { get; set; }

        public bool AnexarArquivo { get; set; }

        public string TituloEmail { get; set; }

        public string CorpoEmail { get; set; }

        [BsonIgnore]

        public string StatusCotistas { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEmailRecebido { get; set; }

        public string DataRecebimentoEmail { get; set; }

        public bool EmailRecebido { get; set; }
    }

    public enum StatusCotacao
    {
        Edicao = 0,
        Enviada = 1,
        Finalizada = 2,
        Cancelada = 3,
    }

    public enum StatusAprovacao
    {
        Aprovado = 0,
        EmEdicao = 1,
        AguardandoAprovacao = 2,
        Recusado = 3
    }
}
