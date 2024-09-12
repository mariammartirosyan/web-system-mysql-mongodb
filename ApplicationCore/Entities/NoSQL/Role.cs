using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class Role
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}

