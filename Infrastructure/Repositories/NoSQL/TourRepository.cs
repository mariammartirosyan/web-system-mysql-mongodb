using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ApplicationCore.Entities.NoSQL;

namespace Infrastructure.Repositories.NoSQL
{
	public class TourRepository
	{
        private readonly IMongoDatabase _mongoDatabase;

        public TourRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public Tour GetById(int id)
        {
            var toursCollection = _mongoDatabase.GetCollection<Tour>("tours");
            return toursCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Tour> GetAll()
        {
            var toursCollection = _mongoDatabase.GetCollection<Tour>("tours");
            return toursCollection.Find(x => true).ToList();
        }
    }
}

