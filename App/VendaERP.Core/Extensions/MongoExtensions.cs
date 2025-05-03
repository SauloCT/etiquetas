using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

public static class MongoExtensions
{
    public static BsonDocument RenderToBsonDocument<T>(this FilterDefinition<T> filter)
    {
        var serializerRegistry = BsonSerializer.SerializerRegistry;
        var documentSerializer = serializerRegistry.GetSerializer<T>();
        return filter.Render(documentSerializer, serializerRegistry);
    }
}