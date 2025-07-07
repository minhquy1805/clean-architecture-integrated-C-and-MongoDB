using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities
{
    public class LoginHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LoginId { get; set; } = ObjectId.GenerateNewId().ToString();

        public string UserId { get; set; } = default!;

        public bool IsSuccess { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
