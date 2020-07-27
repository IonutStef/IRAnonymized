using MongoDB.Bson.Serialization.Attributes;

namespace IRAnonymized.Assignment.Data.Dtos
{
    /// <summary>
    /// StoreItem Entity to be saved in the database.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class StoreItemDto
    {
        public string Key { get; set; }

        public string ArticleCode { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int DiscountPrice { get; set; }

        public string DeliveredInAddress { get; set; }

        public string SizeGroup { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }
    }
}