using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngine.API.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonElement("Id")]
        public string Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("firstName")]
        public string firstName { get; set; }
        [BsonElement("secondName")]
        public string secondName { get; set; }
        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }
        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

    }
}
