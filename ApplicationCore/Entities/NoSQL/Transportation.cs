using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class Transportation
    {
        [BsonElement("departureLocation")]
        public string DepartureLocation { get; set; }
        [BsonElement("departureDateTime")]
        public DateTime DepartureDateTime { get; set; }
        [BsonElement("arrivalLocation")]
        public string ArrivalLocation { get; set; }
        [BsonElement("arrivalDateTime")]
        public DateTime ArrivalDateTime { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}

