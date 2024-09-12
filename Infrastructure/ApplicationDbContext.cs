using System.Data;
using ApplicationCore.Entities;
using ApplicationCore.Entities.MySQL;
using Infrastructure.Helpers;
using Infrastructure.Helpers.CsvMappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Accommodation> Accommodation { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AttractionType> AttractionType { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<TouristAttraction> TouristAttraction { get; set; }
        public DbSet<Transportation> Transportation { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Define Column Types

            modelBuilder.Entity<Accommodation>(
                eb =>
                {
                    eb.Property(b => b.Location).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.Type).HasColumnType("nvarchar(20)");
                    eb.Property(b => b.Price).HasColumnType("decimal(18,2)");
                });
            modelBuilder.Entity<Address>(
                eb =>
                {
                    eb.Property(b => b.Country).HasColumnType("nvarchar(20)");
                    eb.Property(b => b.City).HasColumnType("nvarchar(20)");
                    eb.Property(b => b.Street).HasColumnType("nvarchar(50)");
                });
            modelBuilder.Entity<AttractionType>(
                eb =>
                {
                    eb.Property(b => b.Name).HasColumnType("nvarchar(50)");
                    eb.Property(b => b.Description).HasColumnType("nvarchar(200)");
                });

            modelBuilder.Entity<Booking>(
                eb =>
                {
                    eb.Property(b => b.Price).HasColumnType("decimal(18,2)");
                });
            modelBuilder.Entity<Booking>()
                .HasKey(c => new { c.UserId, c.TourId,c.BookingDate });
            modelBuilder.Entity<Role>(
                eb =>
                {
                    eb.Property(b => b.Name).HasColumnType("nvarchar(20)");
                });
            modelBuilder.Entity<Tour>(
                eb =>
                {
                    eb.Property(b => b.Description).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.Status).HasColumnType("nvarchar(20)");
                    eb.Property(b => b.Price).HasColumnType("decimal(18,2)");
                });
            modelBuilder.Entity<TouristAttraction>(
                eb =>
                {
                    eb.Property(b => b.Name).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.Location).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.Description).HasColumnType("nvarchar(200)");
                });
            modelBuilder.Entity<Transportation>(
                eb =>
                {
                    eb.Property(b => b.DepartureLocation).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.ArrivalLocation).HasColumnType("nvarchar(100)");
                    eb.Property(b => b.Type).HasColumnType("nvarchar(20)");
                    eb.Property(b => b.Price).HasColumnType("decimal(18,2)");
                });
            modelBuilder.Entity<User>(
                 eb =>
                 {
                     eb.Property(b => b.FirstName).HasColumnType("nvarchar(30)");
                     eb.Property(b => b.LastName).HasColumnType("nvarchar(30)");
                     eb.Property(b => b.Email).HasColumnType("nvarchar(50)");
                     eb.Property(b => b.Password).HasColumnType("nvarchar(20)");
                 });
            #endregion

            #region Define the Many-to-many Relationships
            modelBuilder.Entity<User>().HasMany(x => x.Role).WithMany(x=>x.User);
            modelBuilder.Entity<TouristAttraction>().HasMany(x => x.AttractionType).WithMany(x => x.TouristAttraction);
            modelBuilder.Entity<Tour>().HasMany(x => x.IncludedTour).WithMany(x => x.SupersetTour);
            modelBuilder.Entity<Tour>().HasMany(x => x.TouristAttraction).WithMany(x => x.Tour);
            #endregion
        }
        public void DeleteData()
        {
            Set<Booking>().ExecuteDelete();
            Set<Accommodation>().ExecuteDelete();
            Set<Transportation>().ExecuteDelete();
            Set<Tour>().ExecuteDelete();
            Set<TouristAttraction>().ExecuteDelete();
            Set<AttractionType>().ExecuteDelete();
            Set<User>().ExecuteDelete();
            Set<Role>().ExecuteDelete();
            Set<Address>().ExecuteDelete();
            SaveChanges();
        }

        public void InsertData()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Set<Address>().AddRange(CsvDataReader<Address, AddressMap>.GetData($"{baseDirectory}Seed Data/Addresses.csv"));

            var roles = CsvDataReader<Role, RoleMap>.GetData($"{baseDirectory}Seed Data/Roles.csv");
            var users = CsvDataReader<User, UserMap>.GetData($"{baseDirectory}Seed Data/Users.csv");
            var roleUsers = CsvDataReader<RoleUser, RoleUserMap>.GetData($"{baseDirectory}/Seed Data/RolesUsers.csv");

            foreach(var user in users)
            {
                var userRoleIds = roleUsers.Where(x => x.UserId == user.Id).Select(x=>x.RoleId);
                user.Role.AddRange(roles.Where(x => userRoleIds.Contains(x.Id)).ToList());
            }

            Set<Role>().AddRange(roles);
            Set<User>().AddRange(users);

            var attractionTypes = CsvDataReader<AttractionType, AttractionTypeMap>.GetData($"{baseDirectory}Seed Data/AttractionTypes.csv");
            var attractions = CsvDataReader<TouristAttraction, TouristAttractionMap>.GetData($"{baseDirectory}Seed Data/TouristAttractions.csv");
            var attractionAttractionTypeMapping = CsvDataReader<AttractionTypeTouristAttraction, AttractionTypeTouristAttractionMap>
                .GetData($"{baseDirectory}Seed Data/AttractionTypesTouristAttractions.csv");

            foreach(var attraction in attractions)
            {
                var attractionTypeIds = attractionAttractionTypeMapping.Where(x => x.TouristAttractionId == attraction.Id).Select(x => x.AttractionTypeId);
                attraction.AttractionType.AddRange(attractionTypes.Where(x=>attractionTypeIds.Contains(x.Id)));
            }

            Set<AttractionType>().AddRange(attractionTypes);
            Set<TouristAttraction>().AddRange(attractions);

            var attractionsCount = attractions.ToList().Count;
            var rnd = new Random();

            var toursCount = rnd.Next(30, 51);
            var toursSeedData = new List<Tour>();

            int transportationId = 0;
            var transportations = CsvDataReader<Transportation, TransportationMap>.GetData($"{baseDirectory}Seed Data/Transportations.csv");
            var transportationsSeedData = new List<Transportation>();

            int accommodationId = 0;
            var accommodations = CsvDataReader<Accommodation, AccommodationMap>.GetData($"{baseDirectory}Seed Data/Accommodations.csv");
            var accommodationsSeedData = new List<Accommodation>();

            for (int i = 1; i <= toursCount; i++)
            {
                var tour = new Tour() { Id = i };

                //add attractions
                var tourAttrCount = rnd.Next(1, 6);
                for (int j = 1; j <= tourAttrCount; j++)
                {
                    int rndAttrId = rnd.Next(1, attractionsCount + 1);
                    while (tour.TouristAttraction.Where(x => x.Id == rndAttrId).Any())
                    {
                        rndAttrId = rnd.Next(1, attractionsCount + 1);
                    }

                    tour.TouristAttraction.Add(attractions.Where(x => x.Id == rndAttrId).First());
                }

                var includeTours = rnd.Next(0, 2);
                if (toursSeedData.Count > 0 && includeTours == 1)
                {
                    var includedToursCount = rnd.Next(2, 6);
                    includedToursCount = toursSeedData.Count < includedToursCount ? toursSeedData.Count : includedToursCount;

                    for (int j = 1; j <= includedToursCount; j++)
                    {
                        int rndTourId = rnd.Next(1, toursSeedData.Count() + 1);
                        while (rndTourId == i || tour.IncludedTour.Where(x => x.Id == rndTourId).Any())
                        {
                            rndTourId = rnd.Next(1, toursSeedData.Count() + 1);
                        }

                        var includedTour = toursSeedData.Where(x => x.Id == rndTourId).First();
                        tour.IncludedTour.Add(includedTour);
                        includedTour.SupersetTour.Add(tour);
                    }
                    if (includedToursCount > 0)
                    {
                        tour.StartDate = tour.IncludedTour.Min(x => x.StartDate);
                        tour.EndDate = tour.IncludedTour.Max(x => x.EndDate);
                        tour.Price = tour.IncludedTour.Sum(x => x.Price);
                    }
                }
                else
                {
                    tour.StartDate = RandomDateTimeGenerator.Generate(DateTime.Today, 730);
                    tour.EndDate = RandomDateTimeGenerator.Generate(tour.StartDate.AddDays(2), 10);

                    var accommodationRndNum = rnd.Next(1, 3);
                    Accommodation prevAccommodation = null;
                    for (int t = 1; t <= accommodationRndNum; t++)
                    {
                        var rndAcc = accommodations.ElementAt(rnd.Next(0, accommodations.Count()));
                        var accommodation = new Accommodation()
                        {
                            Id = ++accommodationId,
                            Location = rndAcc.Location,
                            Type = rndAcc.Type,
                            Price = rndAcc.Price,
                            TourId = tour.Id
                        };

                        tour.Price += accommodation.Price;

                        //set the date depending on the tour and previous accommodation's date
                        accommodation.CheckInDateTime = prevAccommodation == null ? RandomDateTimeGenerator.Generate(tour.StartDate, tour.EndDate.Subtract(tour.StartDate).Days - 2) :
                            RandomDateTimeGenerator.Generate(prevAccommodation.CheckOutDateTime, tour.EndDate.Subtract(prevAccommodation.CheckOutDateTime).Days);
                        accommodation.CheckOutDateTime = accommodation.CheckInDateTime.AddDays(1);

                        accommodationsSeedData.Add(accommodation);
                        prevAccommodation = accommodation;
                    }

                    var transportRndNum = rnd.Next(1, 3);
                    for (int t = 1; t <= transportRndNum; t++)
                    {
                        var rndTransportation = transportations.ElementAt(rnd.Next(0, transportations.Count()));
                        var transportation = new Transportation()
                        {
                            Id = ++transportationId,
                            TourId = tour.Id,
                            DepartureLocation = rndTransportation.DepartureLocation,
                            ArrivalLocation = rndTransportation.ArrivalLocation,
                            Type = rndTransportation.Type,
                            Price = rndTransportation.Price
                        };
                        tour.Price += transportation.Price;
                        transportation.DepartureDateTime = RandomDateTimeGenerator.Generate(tour.StartDate, tour.EndDate.Subtract(tour.StartDate).Days - 1);
                        transportation.ArrivalDateTime = RandomDateTimeGenerator.Generate(transportation.DepartureDateTime, tour.EndDate.Subtract(transportation.DepartureDateTime).Days);
                        transportationsSeedData.Add(transportation);
                    }
                }

                tour.Status = Status.Active;
                tour.Description = @$"The tour will take place {tour.StartDate.ToString("dd/MM/yyyy")} - {tour.EndDate.ToString("dd/MM/yyyy")} and will cost {tour.Price}$.";

                toursSeedData.Add(tour);
            }

            Set<Tour>().AddRange(toursSeedData);
            Set<Accommodation>().AddRange(accommodationsSeedData);
            Set<Transportation>().AddRange(transportationsSeedData);

            var bookingsSeedData = new List<Booking>();
            var bookingsCount = rnd.Next(50, 101);

            for (int i = 1; i <= bookingsCount; i++)
            {
                var booking = new Booking()
                {
                    TourId = rnd.Next(1, toursCount + 1),
                    UserId = rnd.Next(1, users.Count() + 1)
                };

                var bookingTour = toursSeedData.Where(x => x.Id == booking.TourId).First();
                booking.Price = bookingTour.Price;
                booking.BookingDate = RandomDateTimeGenerator.Generate(DateTime.Today.AddMonths(-1), bookingTour.StartDate.Subtract(DateTime.Today.AddMonths(-1)).Days);
                bookingsSeedData.Add(booking);
            }
            Set<Booking>().AddRange(bookingsSeedData);

            SaveChanges();

        }

    }
}

