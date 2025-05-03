using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

using System;

using System.Globalization;



namespace MongoRepository
{


    /*public class Decimal128Serializer : DecimalSerializer
    {
        // private fields
        private readonly BsonType _representation;
        private readonly RepresentationConverter _converter;

        public Decimal128Serializer() : base()
        {
        }

        public Decimal128Serializer(BsonType representation) : base(representation)
        {
        }

        public Decimal128Serializer(BsonType representation, RepresentationConverter converter) : base(representation, converter)
        {
        }

        public override decimal Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;

            var bsonType = bsonReader.GetCurrentBsonType();

            switch (bsonType)
            {
                case BsonType.Array:
                    var array = BsonArraySerializer.Instance.Deserialize(context);
                    var bits = new int[4];
                    bits[0] = array[0].AsInt32;
                    bits[1] = array[1].AsInt32;
                    bits[2] = array[2].AsInt32;
                    bits[3] = array[3].AsInt32;
                    return new decimal(bits);

                case BsonType.Decimal128:
                    //Bug do MongoDB: https://jira.mongodb.org/browse/CSHARP-2001 
                    var temp = bsonReader.ReadDecimal128().ToString();
                    var truncAt = temp.IndexOf('.') + 13;
                    temp = temp.Substring(0, truncAt > temp.Length ? temp.Length : truncAt);
                    decimal value;
                    if (decimal.TryParse(temp, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                        value = Math.Round(value, 11);
                    return Converter.ToDecimal(value);

                case BsonType.Double:
                    var @double = bsonReader.ReadDouble();
                    if (double.IsInfinity(@double) || double.IsNaN(@double))
                        @double = 0;
                    return Converter.ToDecimal(@double);

                case BsonType.Int32:
                    return Converter.ToDecimal(bsonReader.ReadInt32());

                case BsonType.Int64:
                    return Converter.ToDecimal(bsonReader.ReadInt64());

                case BsonType.String:
                    return JsonConvert.ToDecimal(bsonReader.ReadString());

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }
    }

    public class CustomSerializationProvider : IBsonSerializationProvider
    {
        private static readonly Decimal128Serializer _decimalSerializer = new Decimal128Serializer(BsonType.Decimal128, new RepresentationConverter(true, true));
        private static readonly NullableSerializer<decimal> _nullableDecimalSerializer = new NullableSerializer<decimal>(new Decimal128Serializer(BsonType.Decimal128, new RepresentationConverter(true, true)));

        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(decimal)) return _decimalSerializer;
            if (type == typeof(decimal?)) return _nullableDecimalSerializer;

            return null; // falls back to Mongo defaults
        }
    }*/
}
