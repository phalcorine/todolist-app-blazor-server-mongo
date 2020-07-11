using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Mongo
{
    class MongoConnection
    {
        private string _dbname = "taskify";

        /// <summary>
        /// Represents a connection to the database
        /// </summary>
        private IMongoDatabase db;

        public MongoConnection()
        {
            // Create a mongo client
            var client = new MongoClient();

            // Get a connection to the database and store in our 'db' object
            db = client.GetDatabase(_dbname);
        }

        public IMongoDatabase GetMainDatabase()
        {
            return db;
        }
    }
}
