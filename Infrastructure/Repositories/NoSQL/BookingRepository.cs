using System;
using ApplicationCore.Entities.NoSQL;
using MongoDB.Driver;

namespace Infrastructure.Repositories.NoSQL
{
	public class BookingRepository
	{
        private readonly IMongoDatabase _mongoDatabase;

        public BookingRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public void Insert(UserBooking booking, int userId)
		{
            var usersCollection = _mongoDatabase.GetCollection<User>("users");
            var user = usersCollection.Find(x => x.Id == userId).FirstOrDefault();

            if (user != null)
            {
                user.Bookings.Add(booking);
                var filter = Builders<User>.Filter.Eq(s => s.Id, userId);
                var result = usersCollection.ReplaceOne(filter, user);

            }

            var toursCollection = _mongoDatabase.GetCollection<Tour>("tours");
            var tour = toursCollection.Find(x => x.Id == booking.TourId).FirstOrDefault();

            if (tour != null)
            {
                tour.Bookings.Add(new TourBooking()
                {
                    BookingDate = booking.BookingDate,
                    Price = booking.Price
                });


                var filter = Builders<Tour>.Filter.Eq(s => s.Id, booking.TourId);
                var result = toursCollection.ReplaceOne(filter, tour);
            }
        }

        public List<UserBooking> GetUserBookings(int userId)
        {
            var usersCollection = _mongoDatabase.GetCollection<User>("users");
            var user = usersCollection.Find(x => x.Id == userId).FirstOrDefault();
            return user.Bookings;
        }
    }
}

