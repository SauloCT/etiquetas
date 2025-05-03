
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VendaERP.Core
{
    public interface IRepositoryLastUpdate<T> where T : IEntityLastUpdate
    {
        IMongoCollection<T> Collection { get; }

        T GetById(string id);

        T GetSingle(Expression<Func<T, bool>> criteria);

        IQueryable<T> All();

        IQueryable<T> All(Expression<Func<T, bool>> criteria);

        T Add(T entity);

        T Update(T entity);

        void Delete(string id);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> criteria);

        long Count();

        bool Exists(Expression<Func<T, bool>> criteria);
    }
}
