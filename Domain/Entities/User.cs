using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = ObjectId.GenerateNewId().ToString();

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public string Role { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Gender { get; set; }

        public bool IsActive { get; set; }

        public string? Flag { get; set; }

        // Extra fields
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
        public string? Field5 { get; set; }
    }
}
