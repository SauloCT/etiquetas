using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace VendaERP.Core
{
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class EntityLastUpdate : IEntityLastUpdate
    {
 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastUpdate { get; set; }
    }
}