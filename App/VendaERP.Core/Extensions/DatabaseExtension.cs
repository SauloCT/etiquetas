using MongoDB.Driver;
using System;

namespace VendaERP.Core
{
    public static class DatabaseExtension
    {

        public static IMongoCollection<TEntity> GetCollection<TEntity>(this IMongoDatabase db, string name)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            return db.GetCollection<TEntity>(name);
        }

        public static IMongoCollection<TEntity> GetCollection<TEntity>(this IMongoDatabase db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            return db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

       /* public static IMongoCollection<TEntity> GetCollection<TEntity>(this IMongoDatabase db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            return db.GetCollection<TEntity>(typeof(TEntity).Name);
        }*/
    }
}