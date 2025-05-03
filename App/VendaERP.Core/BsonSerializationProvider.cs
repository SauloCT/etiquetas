using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace VendaERP.Core
{
    /*public class BsonSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(decimal))
                return new DecimalSerializer(BsonType.Double);
            if (type == typeof(decimal?))
                return new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Double));

            return null;
        }
    }*/
}
