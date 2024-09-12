using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class TourBooking
    {
        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}

