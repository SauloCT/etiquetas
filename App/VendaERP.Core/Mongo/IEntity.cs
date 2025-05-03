using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core
{
    public interface IEntity
    {
        [BsonId]
        string Id { get; set; }
    }
}