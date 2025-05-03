using MongoDB.Bson;
using MongoDB.Driver;

namespace VendaERP.Core
{
    public static class MongoRepositoryExtensions
    {


        public static T GetById<T>(this MongoRepository<T> repo, string id, string[] fields) where T : IEntity
        {

            FindOptions options = new FindOptions();

            

            return repo.Collection.Find("{ _id: ObjectId(\"" + id + "\")}").FirstOrDefault();


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
