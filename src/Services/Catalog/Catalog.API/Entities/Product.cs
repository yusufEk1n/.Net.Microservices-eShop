using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("product_name"), BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; }
        [BsonElement("product_category"), BsonRepresentation(BsonType.String)]
        public string Category { get; set; }
        [BsonElement("product_summary"), BsonRepresentation(BsonType.String)]
        public string Summary { get; set; }
        [BsonElement("product_description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonElement("product_imageFile"), BsonRepresentation(BsonType.String)]
        public string ImageFile { get; set; }
        [BsonElement("product_price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        //Nullable property set 
    }
}