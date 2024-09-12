using System;
using ApplicationCore.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class Tour
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("price")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }
        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("bookings")]
        public List<TourBooking> Bookings { get; set; } = new List<TourBooking>();
        [BsonElement("accommodations")]
        public List<Accommodation> Accommodations { get; set; } = new List<Accommodation>(); 
        [BsonElement("transportations")]
        public List<Transportation> Transportations { get; set; } = new List<Transportation>();
        [BsonElement("includedTourIds")]
        public List<int> IncludedTourIds { get; set; } = new List<int>();
        [BsonElement("touristAttractionIds")]
        public List<int> TouristAttractionIds { get; set; } = new List<int>();
    }
}

