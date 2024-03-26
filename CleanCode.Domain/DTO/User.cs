using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.DTO
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; } = string.Empty;

        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonRequired]
        public string Email { get; set; } = string.Empty;

        [BsonRequired]
        public string Password { get; set; } = string.Empty;

        [BsonRequired]
        public string Contact { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool? IsDeleted { get; set; }
    }

    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
