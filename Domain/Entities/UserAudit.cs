using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class UserAudit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuditId { get; set; } = ObjectId.GenerateNewId().ToString();

        public string UserId { get; set; } = default!;

        public string Action { get; set; } = default!;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string? IpAddress { get; set; }

        public string? Flag { get; set; }

        public string? Field1 { get; set; }

        public string? Field2 { get; set; }

        public string? Field3 { get; set; }

        public string? Field4 { get; set; }

        public string? Field5 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
