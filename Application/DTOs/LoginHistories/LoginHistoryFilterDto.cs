using Application.DTOs.Abstract;


namespace Application.DTOs.LoginHistories
{
    public class LoginHistoryFilterDto : BasePagingFilterDto
    {
        public string? UserId { get; set; }
        public bool? IsSuccess { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Message { get; set; }

        public DateTime? FromDate { get; set; } // ✅ thêm nếu cần lọc theo thời gian
        public DateTime? ToDate { get; set; }

    }
}
