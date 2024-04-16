using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.DTO
{
    public class Cart
    {
        [BsonId]
        public string CartId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public List<string> ProductIds { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool? IsDeleted { get; set; }
    }
}
