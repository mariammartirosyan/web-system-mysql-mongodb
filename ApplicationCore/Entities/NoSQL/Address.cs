using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class Address
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("zipCode")]
        public int ZipCode { get; set; }
    }
}

