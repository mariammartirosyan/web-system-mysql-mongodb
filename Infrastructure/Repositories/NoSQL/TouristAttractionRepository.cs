using System;
using ApplicationCore.Entities.NoSQL;
using MongoDB.Driver;

namespace Infrastructure.Repositories.NoSQL
{
	public class TouristAttractionRepository
	{
        private readonly IMongoDatabase _mongoDatabase;

        public TouristAttractionRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public TouristAttraction GetById(int id)
        {
            var touristAttractions = _mongoDatabase.GetCollection<TouristAttraction>("touristAttractions");
            return touristAttractions.Find(x => x.Id == id).FirstOrDefault();
        }
    }
}

