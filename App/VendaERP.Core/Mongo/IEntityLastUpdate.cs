using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace VendaERP.Core
{
    public interface IEntityLastUpdate
    {
        [BsonId]
       
        string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        DateTime LastUpdate { get; set; }
    }
}