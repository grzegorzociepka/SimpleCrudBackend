using AdEngine.API.Helpers;
using AdEngine.API.Helpers.Contexts;
using AdEngine.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdEngine.API.Services
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(string id);
        UserModel Create(UserModel user, string password);
        void Update(UserModel user, string password = null);
        void Delete(string id);
    }
    public class UserService : IUserService
    {
        private UserContext _context;
        public UserService(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }
        public UserModel Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _context.Users.Find(x => x.Username == username).FirstOrDefaultAsync().Result;

            if (user == null)
                throw new Exception("Username does not exist");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid Password");

            return user;
        }

        public UserModel Create(UserModel user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            if (_context.Users.Find(x => x.Username == user.Username).FirstOrDefaultAsync().Result != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.InsertOneAsync(user);

            // _context.SaveChanges();

            return user;
        }

        public void Delete(string id)
        {
            _context.Users.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _context.Users.Find(_ => true).ToList();
        }

        public UserModel GetById(string id)
        {
            return _context.Users.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Update(UserModel userParam, string password = null)
        {
            var filter = Builders<UserModel>.Filter.Eq(s => s.Id, userParam.Id);
            var body = Builders<UserModel>.Update
                .Set(s => s.firstName, userParam.firstName)
                .Set(s => s.secondName, userParam.secondName);
            var user = _context.Users.Find(userParam.Id).FirstOrDefault();
            if (user == null)
                throw new AppException("User not found");
            if (userParam.Username != user.Username)
            {
                if (_context.Users.Find(x => x.Username == userParam.Username).FirstOrDefault() != null)
                    throw new AppException("Username" + userParam.Username + "is already taken");
            }

            user.firstName = userParam.firstName;
            user.secondName = userParam.secondName;
            user.Username = userParam.Username;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _context.Users.UpdateOne(filter,body);
            //_context.Users.Update(user);
            //_context.SaveChanges();
        }

        //private methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA256(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
