using Application.DTOs.Abstract;

namespace Application.DTOs.Users.Filters
{
    public class UserFilterDto : BasePagingFilterDto
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public bool? IsActive { get; set; }
        public string? Flag { get; set; }

    }
}
