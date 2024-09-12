using ApplicationCore.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

using NoSqlEntities = ApplicationCore.Entities.NoSQL;

namespace Infrastructure.Reports.NoSQL
{
    public class ReportGenerator
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ReportGenerator(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public List<ReportRow> Generate()
        {
            var toursCollection = _mongoDatabase.GetCollection<NoSqlEntities.Tour>("tours");

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$addFields",
                    new BsonDocument("numberOfBookings",
                        new BsonDocument("$size", "$bookings")
                    )
                ),

                new BsonDocument("$match", new BsonDocument
                    {
                        { "numberOfBookings", new BsonDocument("$gt", 0) },
                        { "price", new BsonDocument("$lt", 500) }
                    }
                ),

                new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "touristAttractions" },
                        { "localField", "touristAttractionIds" },
                        { "foreignField", "_id" },
                        { "as", "attractions" }
                    }
                ),

                new BsonDocument("$unwind", "$attractions"),
                new BsonDocument("$unwind", "$attractions.attractionTypes"),

                new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", new BsonDocument
                            {
                                { "tourId", "$_id" },
                                { "attractionName", "$attractions.name" },
                                { "attractionLocation", "$attractions.location" },
                                { "price", "$price" },
                                { "attractionTypeName", "$attractions.attractionTypes.name" }
                            }
                        },
                        { "numberOfBookings", new BsonDocument("$first", "$numberOfBookings") },
                        { "description", new BsonDocument("$first", "$description") }
                    }
                ),

                new BsonDocument("$match", new BsonDocument
                    {
                        { "_id.attractionTypeName", "Natural Landscape" }
                    }
                ),

                new BsonDocument("$sort", new BsonDocument
                    {
                        { "numberOfBookings", -1 },
                        { "_id.tourId", -1 }
                    }
                ),

                new BsonDocument("$project", new BsonDocument
                    {
                        { "_id", 0 },
                        { "tourId", "$_id.tourId" },
                        { "numberOfBookings", 1 },
                        { "description", 1 },
                        { "price", "$_id.price" },
                        { "attractionTypeName", "$_id.attractionTypeName" },
                        { "attractionName", "$_id.attractionName" },
                        { "attractionLocation", "$_id.attractionLocation" }
                    }
                ),
            };
            var results = toursCollection.Aggregate<BsonDocument>(pipeline).ToList();

            var reportRows = new List<ReportRow>();
            foreach (var row in results)
            {
                reportRows.Add(new ReportRow()
                {
                    TourId = row["tourId"].AsInt32,
                    BookingsNumber = row["numberOfBookings"].AsInt32,
                    Description = row["description"].AsString,
                    Price = (decimal)row["price"].AsNullableDecimal,
                    AttractionTypeName = row["attractionTypeName"].AsString,
                    AttractionName = row["attractionName"].AsString,
                    AttractionLocation = row["attractionLocation"].AsString
                });
            }
            return reportRows;
        }
    }
}

