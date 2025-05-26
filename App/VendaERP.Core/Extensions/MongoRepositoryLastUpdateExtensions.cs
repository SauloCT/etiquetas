using MongoDB.Bson;
using MongoDB.Driver;

namespace VendaERP.Core
{
    public static class MongoRepositoryLastUpdateExtensions
    {
        public static T GetById<T>(this MongoRepositoryLastUpdate<T> repo, string id, string[] fields) where T : IEntityLastUpdate
        {
            // Validação de entrada para prevenir injection
            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {
                return default(T);
            }
            
            // Uso de filtro tipado ao invés de string concatenation
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return repo.Collection.Find(filter).FirstOrDefault();
        }

        public static T SafeGetById<T>(this MongoRepositoryLastUpdate<T> repository, string id) where T : IEntityLastUpdate
        {
            try
            {
                return repository.GetById(id);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
