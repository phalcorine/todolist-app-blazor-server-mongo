using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Mongo.CrudHelpers
{
    class CRUD
    {
        private static CRUD _instance = null;
        private IMongoDatabase database;
        public CRUD()
        {
            // Get a connection to the main database
            database = new MongoConnection().GetMainDatabase();
        }

        /// <summary>
        /// Gets an instance of the containing class from memory.
        /// If one doesn't exists, a new instance is stored and retrieved
        /// immediately.
        /// </summary>
        /// <returns></returns>
        public static CRUD GetInstance()
        {
            if(_instance == null)
            {
                _instance = new CRUD();
            }

            return _instance;
        }

        public IMongoCollection<T> GetCollection<T>(string table)
        {
            var collection = database.GetCollection<T>(table);

            return collection;
        }

        /// <summary>
        /// Fetches all records in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<T> Find<T>(string table)
        {
            var collection = database.GetCollection<T>(table);

            var records = collection.Find(new BsonDocument()).ToList();

            return records;
        }

        /// <summary>
        /// Inserts a record into the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="record"></param>
        public void InsertOne<T>(string table, T record)
        {
            var collection = database.GetCollection<T>(table);

            collection.InsertOne(record);
        }

        /// <summary>
        /// Inserts many records at once into the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="records"></param>
        public void InsertMany<T>(string table, IEnumerable<T> records)
        {
            var collection = database.GetCollection<T>(table);

            collection.InsertMany(records);
        }

        public bool UpdateOne<T>(string table, Guid id, T record)
        {
            var collection = database.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                    filter: Builders<T>.Filter.Eq("Id", id),
                    replacement: record,
                    new ReplaceOptions { IsUpsert = true }
                );

            return result.IsAcknowledged == true;
        }

        /// <summary>
        /// Updates many records from the collection based on a set of filters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="filter"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public bool UpdateMany<T>(string table, FilterDefinition<T> filter, T records)
        {
            // @TODO: Fix logic
            return true;
        }

        /// <summary>
        /// Deletes a single record from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOne<T>(string table, Guid id)
        {
            var collection = database.GetCollection<T>(table);

            var result = collection.DeleteOne(
                    filter: Builders<T>.Filter.Eq("Id", id)
                );

            return result.IsAcknowledged == true;
        }

        /// <summary>
        /// Deletes many records from the collection based on a set of filters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteMany<T>(string table, FilterDefinition<T> filter)
        {
            // @TODO: Fix logic
            return true;
        }

    }
}
