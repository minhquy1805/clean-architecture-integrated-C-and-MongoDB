namespace Application.DTOs.LoginHistories
{
    public class LoginHistoryDto
    {
        public string? LoginId { get; set; }
        public string? UserId { get; set; }
        public bool IsSuccess { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
