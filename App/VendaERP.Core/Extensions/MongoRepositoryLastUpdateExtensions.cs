using MongoDB.Bson;
using MongoDB.Driver;

namespace VendaERP.Core
{
    public static class MongoRepositoryLastUpdateExtensions
    {
        public static T GetById<T>(this MongoRepositoryLastUpdate<T> repo, string id, string[] fields) where T : IEntityLastUpdate
        {
            return repo.Collection.Find("{ _id: ObjectId(\"" + id + "\")}").FirstOrDefault();
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
