

namespace Application.DTOs.Users.Requests
{
    public class ResetPasswordRequest
    {
        public string? UserId { get; set; }
        public string Token { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
