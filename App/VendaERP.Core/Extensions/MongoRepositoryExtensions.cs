using MongoDB.Bson;
using MongoDB.Driver;

namespace VendaERP.Core
{
    public static class MongoRepositoryExtensions
    {


        public static T GetById<T>(this MongoRepository<T> repo, string id, string[] fields) where T : IEntity
        {
            // Validação de entrada para prevenir injection
            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {
                return default(T);
            }

            FindOptions options = new FindOptions();
            
            // Uso de filtro tipado ao invés de string concatenation
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return repo.Collection.Find(filter).FirstOrDefault();
        }

        public static T SafeGetById<T>(this MongoRepository<T> repository, string id) where T : Entity
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
