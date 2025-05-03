namespace VendaERP.Core
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Configuration;
    using System.Web;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using MongoDB.Bson.Serialization.Conventions;

    /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        /// <summary>
        /// MongoCollection field.
        /// </summary>
        private IMongoCollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        /// 

        #region Contructors
        /// <summary>
        /// Metodo que possui parametro para informar atributo para função de max
        /// </summary>
        /// <param name="url"></param>
        /// <param name="atributeForMaxFunction">Nome do atributo da entidade a ser usado na função de Max</param>
        public MongoRepository(MongoUrl url)
        {
            this.Init(url);
        }

        public MongoRepository(IMongoDatabase database)
        {
            this.collection = database.GetCollection<T>(typeof(T).Name);
        }

        private void Init(MongoUrl url)
        {
            ConventionPack conventionPack = new ConventionPack();
            conventionPack.Add(new IgnoreExtraElementsConvention(ignoreExtraElements: true));
            ConventionRegistry.Register("VendaERP Conventions", conventionPack, (Type t) => true);

            MongoClient mongoClient = new MongoClient(url);
            this.collection = mongoClient.GetDatabase(url.DatabaseName).GetCollection<T>(typeof(T).Name);
        }

        #endregion

        /// <summary>
        /// Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        /// One can argue that exposing this property (and with that, access to it's Database property for instance
        /// (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        /// for most purposes you can use the MongoRepositoryManager<T>
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        public IMongoCollection<T> Collection
        {
            get
            {
                return this.collection;
            }
        }

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The string representing the ObjectId of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        public T GetById(string id)
        {

            // CóDIGO POUCO EFEICAZ E QUE CAUSA MORTES

            //#region Ajustes Code Machine
            ////if (string.IsNullOrEmpty(id) || id.Contains(".aspx"))
            ////    return (T)Activator.CreateInstance(typeof(T), null);
            //#endregion

            //return default(T);

            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                this.collection.Find<T>("{_id:ObjectId(\"" + id + "\")}").FirstOrDefault(); 
            }

            return this.collection.Find<T>("{_id:\"" + id + "\"}").FirstOrDefault();
        }

        /// <summary>
        /// Returns a single T by the given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>A single T matching the criteria.</returns>
        public T GetSingle(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().FirstOrDefault(criteria);
        }

        /// <summary>
        /// Returns the list of T where it matches the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>IQueryable of T.</returns>
        public IQueryable<T> All(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().Where(criteria);
        }

        /// <summary>
        /// Returns All the records of T.
        /// </summary>
        /// <returns>IQueryable of T.</returns>
        public IQueryable<T> All()
        {
            return this.collection.AsQueryable<T>();
        }

        /// <summary>
        /// Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity T.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        public T Add(T entity)
        {
            /*this.collection.InsertOne<T>(entity);*/

            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            /*foreach (var entity in entities)
                this.collection.InsertOne<T>(entity);*/

            return entities;
        }


        /// <summary>
        /// Adiciona um itemn sem validar nd e sem registrar log da ação, mais performatico mas mais perigoso
        /// </summary>
        /// <param name="entity">The entity T.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        public T AddSemLog(T entity)
        {
            /*this.collection.Insert<T>(entity);*/

            return entity;
        }


        /// <summary>
        /// Upserts an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The updated entity.</returns>
        public T Update(T entity)
        {
            //FAZ UPDATE SE EXISTE
            if (entity.Id != null)
            {
                /*var oldEntity = this.collection.FindOneById(ObjectId.Parse(entity.Id));
                if (oldEntity != null && !string.IsNullOrEmpty(entity.Id))
                    this.collection.Save<T>(entity);*/
            }

            return entity;
        }


        /// <summary>
        ///Faz update sem validar nd e sem registrar log da ação, mais performatico mas mais perigoso
        /// </summary>
        /// <returns>The updated entity.</returns>
        public T UpdateSemLog(T entity)
        {
            //FAZ UPDATE SE EXISTE
            /*if (entity.Id != null)
                this.collection.Save<T>(entity);*/

            return entity;
        }

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The string representation of the entity's id.</param>
        public void Delete(string id, bool IgnorarPermissaoUsuario)
        {
            /*var entity = this.collection.FindOneById(new ObjectId(id));

            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                this.collection.Remove(Query.EQ("_id", new ObjectId(id)));
            }
            else
            {
                this.collection.Remove(Query.EQ("_id", id));
            }*/
        }



        public void Delete(string id)
        {
            this.Delete(id, false);
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity, bool IgnorarPermissaoUsuario)
        {
            this.Delete(entity.Id);
        }

        public void Delete(T entity)
        {
            this.Delete(entity, false);
        }

        /// <summary>
        /// Deletes the entities matching the criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        public void Delete(Expression<Func<T, bool>> criteria)
        {
            this.Delete(criteria, false);
        }

        public void Delete(Expression<Func<T, bool>> criteria, bool IgnorarPermissaoUsuario)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(criteria))
            {
                this.Delete(entity.Id, IgnorarPermissaoUsuario);
            }
        }

        

        /// <summary>
        /// Checks if the entity exists for given criteria.
        /// </summary>
        /// <param name="criteria">The expression.</param>
        /// <returns>true when an entity matching the criteria exists, false otherwise.</returns>
        public bool Exists(Expression<Func<T, bool>> criteria)
        {
            return this.collection.AsQueryable<T>().Any(criteria);
        }

        public void ImportRange(IEnumerable<T> collection)
        {
            // Do Nothing
        }
    }
}