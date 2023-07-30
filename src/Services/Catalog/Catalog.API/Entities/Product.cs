using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("p_name")]
        public string Name { get; set; }
        [BsonElement("p_category")]
        public string Category { get; set; }
        [BsonElement("p_summary")]
        public string Summary { get; set; }
        [BsonElement("p_description")]
        public string Description { get; set; }
        [BsonElement("p_imageFile")]
        public string ImageFile { get; set; }
        [BsonElement("p_price")]
        public decimal Price { get; set; }
    }
}