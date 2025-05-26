using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;


namespace VendaERP.Core
{
    public class MongoRepositoryLastUpdate<T> : IRepositoryLastUpdate<T> where T : IEntityLastUpdate
    {
        private IMongoCollection<T> collection = null!;

        public IMongoCollection<T> Collection => collection;

        public MongoRepositoryLastUpdate(MongoUrl url)
        {
            Init(url);
        }

        public MongoRepositoryLastUpdate(IMongoDatabase database)
        {
            collection = database.GetCollection<T>(typeof(T).Name);
        }

        private void Init(MongoUrl url)
        {
            ConventionPack conventionPack = new ConventionPack();
            conventionPack.Add(new IgnoreExtraElementsConvention(ignoreExtraElements: true));
            ConventionRegistry.Register("VendaERP Conventions", conventionPack, (Type t) => true);
            MongoClient client = new MongoClient(url);
            
            this.collection = client.GetDatabase(url.DatabaseName).GetCollection<T>(typeof(T).Name);
        }

        public T GetById(string id)
        {
            // Validação de entrada para prevenir injection
            if (string.IsNullOrWhiteSpace(id))
            {
                return default(T);
            }

            if (typeof(T).IsSubclassOf(typeof(EntityLastUpdate)))
            {
                // Validação de ObjectId e uso de filtro tipado
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return default(T);
                }
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                return this.collection.Find(filter).FirstOrDefault();
            }

            // Para entidades que não usam ObjectId, usar filtro tipado também
            var stringFilter = Builders<T>.Filter.Eq("_id", id);
            return this.collection.Find(stringFilter).FirstOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            return collection.AsQueryable().FirstOrDefault(criteria);
        }

        public IQueryable<T> All(Expression<Func<T, bool>> criteria)
        {
            return collection.AsQueryable().Where(criteria);
        }

        public IQueryable<T> All()
        {
            return collection.AsQueryable();
        }

        public T Add(T entity)
        {
            entity.LastUpdate = DateTime.Now;
            collection.InsertOne(entity);
            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            foreach (T entity in entities)
            {
                entity.LastUpdate = DateTime.Now;
            }
            collection.InsertMany(entities);
            return entities;
        }

        public T Update(T entity)
        {
            if (!string.IsNullOrEmpty(entity.Id) && ObjectId.TryParse(entity.Id, out ObjectId objectId))
            {
                entity.LastUpdate = DateTime.Now;
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                collection.ReplaceOne(filter, entity);
            }
            return entity;
        }

        public void Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            if (typeof(T).IsSubclassOf(typeof(EntityLastUpdate)))
            {
                if (ObjectId.TryParse(id, out ObjectId objectId))
                {
                    var filter = Builders<T>.Filter.Eq("_id", objectId);
                    collection.DeleteOne(filter);
                }
            }
            else
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                collection.DeleteOne(filter);
            }
        }

        public void Delete(T entity)
        {
            Delete(entity.Id);
        }

        public void Delete(Expression<Func<T, bool>> criteria)
        {
            foreach (T item in collection.AsQueryable().Where(criteria))
            {
                Delete(item.Id);
            }
        }

        public long Count()
        {   
            return collection.EstimatedDocumentCount();
        }

        public long Count(FilterDefinition<T> query)
        {
            return collection.CountDocuments(query);
        }


        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            return collection.AsQueryable().Any(criteria);
        }

        public void ImportRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }
    }
}
