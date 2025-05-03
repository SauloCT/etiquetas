


using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoNFesMultiVendas : Entity
    {
        public DtoNFesMultiVendas()
        {
            this.VendaIDs = new List<string>();
        }

        public List<string> VendaIDs { get; set; }
    }
}