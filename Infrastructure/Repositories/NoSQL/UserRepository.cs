using System;
using ApplicationCore.Entities.NoSQL;
using MongoDB.Driver;

namespace Infrastructure.Repositories.NoSQL
{
	public class UserRepository
	{
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public User GetById(int id)
        {
            var usersCollection = _mongoDatabase.GetCollection<User>("users");
            return usersCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            var usersCollection = _mongoDatabase.GetCollection<User>("users");
            return usersCollection.Find(x => true).ToList();
        }
    }
}

