

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoTransferenciaBancaria : Entity
    {
        public string BancoOrdemID { get; set; }


        public string BancoOrigem { get; set; }

        public string BancoDestinoID { get; set; }


        public string BancoDestino { get; set; }
        

        public string Documento { get; set; }


        public double Valor { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataTransferencia { get; set; }


        public string Observacoes { get; set; }

    }
}