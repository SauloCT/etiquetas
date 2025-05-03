using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;


namespace VendaERP.Core
{
    public class MongoRepositoryLastUpdate<T> : IRepositoryLastUpdate<T> where T : IEntityLastUpdate
    {
        private IMongoCollection<T> collection;

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
                this.collection.Find<T>("{_id:ObjectId(\"" + id + "\")}").FirstOrDefault();
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
            /*entity.LastUpdate = DateTime.Now;
            ((IMongoCollection)collection).Insert(entity);*/
            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            /*foreach (T entity in entities)
            {
                T current = entity;
                current.LastUpdate = DateTime.Now;
                ((IMongoCollection)collection).Insert(current);
            }*/

            return entities;
        }

        public T Update(T entity)
        {
            /*if (!string.IsNullOrEmpty(entity.Id))
            {
                entity.LastUpdate = DateTime.Now;
                ((IMongoCollection)collection).Save(entity);
            }*/

            return entity;
        }

        public void Delete(string id)
        {

            /*if (typeof(T).IsSubclassOf(typeof(EntityLastUpdate)))
            {
                collection.Remove(Query.EQ("_id", new ObjectId(id)));
            }
            else
            {
                collection.Remove(Query.EQ("_id", id));
            }*/
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
