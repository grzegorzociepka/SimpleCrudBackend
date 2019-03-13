using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngine.API.Controllers
{
    public class MongoController
    {
        public static IMongoClient _client;
        public static IMongoDatabase _database;

        public static IMongoDatabase getDb()
        {
            var client = new MongoClient(IniProperties.mongoUrl);
            IMongoDatabase db = client.GetDatabase(IniProperties.databaseName);
            return db;
        }
        public static async Task MainAsync()
        {
            var db = getDb();
            var collection = db.GetCollection<BsonDocument>("testDB");

            var document = new BsonDocument();
            document.Add("name", "Steven Johnson2");
            document.Add("age", 24);
            document.Add("subjects", new BsonArray() { "Anglish", "Math", "Physics" });
            await collection.InsertOneAsync(document);

            //var mongoClient = new MongoClient(IniProperties.mongoUrl);
            //IMongoDatabase db = mongoClient.GetDatabase("testDB");
            //var document = new BsonDocument
            //    {
            //      {"firstname", BsonValue.Create("Beniz:DDD")}
            //    };

            //IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("students");
            //collection.InsertOneAsync(document);
        }

    }
}
