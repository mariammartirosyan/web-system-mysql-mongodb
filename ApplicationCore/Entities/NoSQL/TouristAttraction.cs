using System;
using ApplicationCore.Entities.MySQL;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities.NoSQL
{
    public class TouristAttraction
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("attractionTypes")]
        public List<AttractionType> AttractionTypes { get; set; } = new List<AttractionType>();
    }
}

