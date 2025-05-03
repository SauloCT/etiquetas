using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueMovEntreDepositos : Entity
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Data { get; set; }     

        public string DepositoOrigemID { get; set; }


        public string DepositoOrigem { get; set; }

        public string DepositoDestinoID { get; set; }


        public string DepositoDestino { get; set; }


        public string NumeroNFe { get; set; }
    }
}