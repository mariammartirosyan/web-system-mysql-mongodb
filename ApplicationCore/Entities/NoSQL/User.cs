using System;
using ApplicationCore.Entities.MySQL;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("address_id")]
        public int AddressId { get; set; }
        [BsonElement("roles")]
        public List<Role> Roles { get; set; }
        [BsonElement("bookings")]
        public List<UserBooking> Bookings { get; set; } = new List<UserBooking>();
    }
}

