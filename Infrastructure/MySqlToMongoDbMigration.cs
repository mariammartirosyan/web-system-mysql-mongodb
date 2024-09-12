using System;
using NoSQL = ApplicationCore.Entities.NoSQL;
using MySQL = ApplicationCore.Entities.MySQL;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure
{
	public class MySqlToMongoDbMigration
	{
        private readonly ApplicationDbContext _context;
        private readonly IMongoDatabase _mongoDatabase;

        public MySqlToMongoDbMigration(ApplicationDbContext context, IMongoDatabase mongoDatabase)
        {
            _context = context;
            _mongoDatabase = mongoDatabase;
        }

        //I have users, touristAttractions, tours, addresses as colllections
        public void MigrateData()
        {
            MigrateUsers();
            MigrateTouristAttractions();
            MigrateTours();
        }

        public void DeleteData()
        {
            _mongoDatabase.GetCollection<NoSQL.User>("users").DeleteMany(x=>true);
            _mongoDatabase.GetCollection<NoSQL.Address>("addresses").DeleteMany(x => true);
            _mongoDatabase.GetCollection<NoSQL.Tour>("touristAttractions").DeleteMany(x => true);
            _mongoDatabase.GetCollection<NoSQL.Tour>("tours").DeleteMany(x => true);
        }

        private void MigrateUsers()
        {
           
            var usersCollection = _mongoDatabase.GetCollection<NoSQL.User>("users");
            var addressesCollection = _mongoDatabase.GetCollection<NoSQL.Address>("addresses");

            var mySqlUsers = _context.Set<MySQL.User>().ToList();

            foreach (var mySqlUser in mySqlUsers)
            {
                if (usersCollection.Find(x => x.Id == mySqlUser.Id).FirstOrDefault() == null)
                {
                    var mongoUser = new NoSQL.User
                    {
                        Id = mySqlUser.Id,
                        FirstName = mySqlUser.FirstName,
                        LastName = mySqlUser.LastName,
                        Email = mySqlUser.Email,
                        Password = mySqlUser.Password,
                        Roles = new List<NoSQL.Role>(),
                        Bookings = new List<NoSQL.UserBooking>()
                    };
                    //add user roles
                    foreach (var userRole in mySqlUser.Role)
                    {
                        mongoUser.Roles.Add(new NoSQL.Role
                        {
                            Name = userRole.Name
                        });
                    }

                    //add user bookings
                    foreach (var booking in mySqlUser.Booking)
                    {
                        mongoUser.Bookings.Add(new NoSQL.UserBooking
                        {
                            TourId = booking.TourId,
                            BookingDate = booking.BookingDate,
                            Price = booking.Price
                        });
                    }

                    var address = addressesCollection.Find(r => r.Id == mySqlUser.Address.Id).FirstOrDefault();

                    if (address == null)
                    {
                        addressesCollection.InsertOne(new NoSQL.Address
                        {
                            Id = mySqlUser.Address.Id,
                            Street = mySqlUser.Address.Street,
                            City = mySqlUser.Address.City,
                            ZipCode = mySqlUser.Address.ZipCode,
                            Country = mySqlUser.Address.Country
                        });
                    }

                    mongoUser.AddressId = mySqlUser.Address.Id;

                    usersCollection.InsertOne(mongoUser);

                    var bookingsIndex = Builders<NoSQL.User>.IndexKeys.Ascending("Bookings");
                    usersCollection.Indexes.CreateOne(new CreateIndexModel<NoSQL.User>(bookingsIndex));
                }
            }
        }

        private void MigrateTours()
        {
            var toursCollection = _mongoDatabase.GetCollection<NoSQL.Tour>("tours");

            var mySqlTours = _context.Set<MySQL.Tour>().ToList();

            foreach (var mySqlTour in mySqlTours)
            {
                if (toursCollection.Find(x => x.Id == mySqlTour.Id).FirstOrDefault() == null)
                {
                    var mongoTour = new NoSQL.Tour
                    {
                        Id = mySqlTour.Id,
                        Price = mySqlTour.Price,
                        Description = mySqlTour.Description,
                        StartDate = mySqlTour.StartDate,
                        EndDate = mySqlTour.EndDate,
                        Status = mySqlTour.Status.ToString(),
                        Bookings = new List<NoSQL.TourBooking>(),
                        Accommodations = new List<NoSQL.Accommodation>(),
                        Transportations = new List<NoSQL.Transportation>(),
                        IncludedTourIds = new List<int>(),
                        TouristAttractionIds = new List<int>()
                    };

                    //add bookings
                    foreach (var mySqlBooking in mySqlTour.Booking)
                    {
                        mongoTour.Bookings.Add(new NoSQL.TourBooking
                        {
                            BookingDate = mySqlBooking.BookingDate,
                            Price = mySqlBooking.Price
                        });
                    }

                    //add accommodations
                    foreach (var mySqlAccommodation in mySqlTour.Accommodation)
                    {
                        mongoTour.Accommodations.Add(new NoSQL.Accommodation
                        {
                            Location = mySqlAccommodation.Location,
                            CheckInDateTime = mySqlAccommodation.CheckInDateTime,
                            CheckOutDateTime = mySqlAccommodation.CheckOutDateTime,
                            Type = mySqlAccommodation.Type,
                            Price = mySqlAccommodation.Price
                        });
                    }

                    //add transportations
                    foreach (var mySqlTransportation in mySqlTour.Transportation)
                    {
                        mongoTour.Transportations.Add(new NoSQL.Transportation
                        {
                            DepartureLocation = mySqlTransportation.DepartureLocation,
                            DepartureDateTime = mySqlTransportation.DepartureDateTime,
                            ArrivalLocation = mySqlTransportation.ArrivalLocation,
                            ArrivalDateTime = mySqlTransportation.ArrivalDateTime,
                            Type = mySqlTransportation.Type,
                            Price = mySqlTransportation.Price
                        });
                    }

                    //add included tour references
                    foreach (var mySqlIncludedTour in mySqlTour.IncludedTour)
                    {
                        mongoTour.IncludedTourIds.Add(mySqlIncludedTour.Id);
                    }

                    //add tourist attraction references
                    foreach (var mySqlTouristAttraction in mySqlTour.TouristAttraction)
                    {
                        mongoTour.TouristAttractionIds.Add(mySqlTouristAttraction.Id);
                    }

                    toursCollection.InsertOne(mongoTour);
                }
            }

            var priceIndex = Builders<NoSQL.Tour>.IndexKeys.Ascending("price");
            toursCollection.Indexes.CreateOne(new CreateIndexModel<NoSQL.Tour>(priceIndex));
        }

        private void MigrateTouristAttractions()
        {
            var touristAttractionsCollection = _mongoDatabase.GetCollection<NoSQL.TouristAttraction>("touristAttractions");
            var mySqlTouristAttractions = _context.Set<MySQL.TouristAttraction>().ToList();

            foreach (var mySqlTouristAttraction in mySqlTouristAttractions)
            {
                if(touristAttractionsCollection.Find(x=>x.Id== mySqlTouristAttraction.Id).FirstOrDefault() == null)
                {
                    var mongoTouristAttraction = new NoSQL.TouristAttraction
                    {
                        Id = mySqlTouristAttraction.Id,
                        Name = mySqlTouristAttraction.Name,
                        Location = mySqlTouristAttraction.Location,
                        Description = mySqlTouristAttraction.Description,
                        AttractionTypes = new List<NoSQL.AttractionType>()
                    };

                    foreach(var mySqlAttractionType in mySqlTouristAttraction.AttractionType)
                    {
                        mongoTouristAttraction.AttractionTypes.Add(new NoSQL.AttractionType()
                        {
                            Description = mySqlAttractionType.Description,
                            Name = mySqlAttractionType.Name
                        });
                    }
                    touristAttractionsCollection.InsertOne(mongoTouristAttraction);

                    var attractionTypeNameIndex = Builders<NoSQL.TouristAttraction>.IndexKeys.Ascending("attractionTypes.name");
                    touristAttractionsCollection.Indexes.CreateOne(new CreateIndexModel<NoSQL.TouristAttraction>(attractionTypeNameIndex));
                }
               
            }
        }
    }
}