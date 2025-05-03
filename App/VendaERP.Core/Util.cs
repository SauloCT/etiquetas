namespace VendaERP.Core
{
    using System;
    using System.Configuration;
    using MongoDB.Driver;

    /// <summary>
    /// Internal miscellaneous utility functions.
    /// </summary>
    internal static class Util
    {
        ///// <summary>
        ///// Creates and returns a MongoDatabase from the specified url.
        ///// </summary>
        ///// <param name="url">The url to use to get the database from.</param>
        ///// <returns>Returns a MongoDatabase from the specified url.</returns>
        //private static MongoDatabase GetDatabaseFromUrl(MongoUrl url)
        //{
        //    var client = new MongoClient(url);
        //    var server = client.GetServer();
        //    //MongoCredentials credentials = new MongoCredentials(ConfigurationSettings.AppSettings["MongoDb_User"], ConfigurationSettings.AppSettings["MongoDb_Pass"], true);
        //    return server.GetDatabase(url.DatabaseName);
        //}


        /*public static MongoDatabase MongoDB;
        public static MongoServer MongoServer;
        private static string LastUrl;

        public static void Setup(MongoUrl url)
        {
            if (LastUrl != url.Url)
            {
                // ultima url usada
                LastUrl = url.Url;
                MongoClient mongoClient = new MongoClient(url);

                MongoServer = mongoClient.GetServer();

                // settiing to connect
                var settings = new MongoDatabaseSettings(MongoServer, url.DatabaseName);

                MongoDB = MongoServer.GetDatabase(url.DatabaseName, settings);

            }
        }*/

        ///// <summary>
        ///// Creates and returns a MongoCollection from the specified type and connectionstring.
        ///// </summary>
        ///// <typeparam name="T">The type to get the collection of.</typeparam>
        ///// <param name="connectionstring">The connectionstring to use to get the collection from.</param>
        ///// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        //public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionstring)
        //    where T : IEntity
        //{
        //    return MongoDB.GetCollection<T>(GetCollectionName<T>());
        //}

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        //public static MongoCollection<T> GetCollection<T>(MongoUrl url)
        //    where T : IEntity
        //{
        //    var client = new MongoClient(url);
        //    var server = client.GetServer();
        //    var settings = new MongoDatabaseSettings(server, url.DatabaseName);

        //    return server.GetDatabase(url.DatabaseName, settings).GetCollection<T>(GetCollectionName<T>());
        //}

        /// <summary>
        /// Determines the collectionname for T and assures it is not empty
        /// </summary>
        /// <typeparam name="T">The type to determine the collectionname for.</typeparam>
        /// <returns>Returns the collectionname for T.</returns>
        private static string GetCollectionName<T>() where T : IEntity
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectioNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        /// <summary>
        /// Determines the collectionname from the specified type.
        /// </summary>
        /// <typeparam name="T">The type to get the collectionname from.</typeparam>
        /// <returns>Returns the collectionname from the specified type.</returns>
        private static string GetCollectioNameFromInterface<T>()
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }

            return collectionname;
        }

        /// <summary>
        /// Determines the collectionname from the specified type.
        /// </summary>
        /// <param name="entitytype">The type of the entity to get the collectionname from.</param>
        /// <returns>Returns the collectionname from the specified type.</returns>
        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                // No attribute found, get the basetype
                while (!entitytype.BaseType.Equals(typeof(Entity)))
                {
                    entitytype = entitytype.BaseType;
                }

                collectionname = entitytype.Name;
            }

            return collectionname;
        }
    }
}
