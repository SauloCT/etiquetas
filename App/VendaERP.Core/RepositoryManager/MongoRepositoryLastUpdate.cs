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
            if (typeof(T).IsSubclassOf(typeof(EntityLastUpdate)))
            {
                return this.collection.Find<T>("{_id:ObjectId(\"" + id + "\")}").FirstOrDefault();
            }

            return this.collection.Find<T>("{_id:\"" + id + "\"}").FirstOrDefault();
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
            if (!string.IsNullOrEmpty(entity.Id))
            {
                entity.LastUpdate = DateTime.Now;
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
                collection.ReplaceOne(filter, entity);
            }
            return entity;
        }

        public void Delete(string id)
        {
            if (typeof(T).IsSubclassOf(typeof(EntityLastUpdate)))
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                collection.DeleteOne(filter);
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
