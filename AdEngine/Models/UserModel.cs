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
        public string Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("First name")]
        public string firstName { get; set; }
        [BsonElement("Second name")]
        public string secondName { get; set; }
        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }
        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

    }
}
