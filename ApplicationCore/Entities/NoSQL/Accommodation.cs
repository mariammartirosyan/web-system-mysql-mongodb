using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class Accommodation
    {
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("checkInDateTime")]
        public DateTime CheckInDateTime { get; set; }
        [BsonElement("checkOutDateTime")]
        public DateTime CheckOutDateTime { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}

