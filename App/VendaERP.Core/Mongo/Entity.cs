using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace VendaERP.Core
{
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}