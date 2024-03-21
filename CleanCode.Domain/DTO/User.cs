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
        public int UserId { get; set; }

        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonRequired]
        public string Email { get; set; } = string.Empty;

        [BsonRequired]
        public string Password { get; set; } = string.Empty;

        [BsonRequired]
        public string Contact { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
