using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.DTO
{
    public class Product
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonRequired]
        public Type_Category Type_Category { get; set; }

        [BsonRequired]
        public Anime_Category Anime_Category { get; set; }

        [BsonRequired]
        public double Price { get; set; }

        [BsonRequired]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool? IsDeleted { get; set; }
    }

    public enum Type_Category
    {
        tshirt,
        hoody,
        sweatshirt
    }
    public enum Anime_Category
    {

    }
}
