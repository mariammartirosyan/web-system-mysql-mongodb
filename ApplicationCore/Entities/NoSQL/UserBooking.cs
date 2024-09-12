using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
	public class UserBooking
	{
        [BsonElement("tourId")]
        public int TourId { get; set; }
        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; }
        [BsonElement("price")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}