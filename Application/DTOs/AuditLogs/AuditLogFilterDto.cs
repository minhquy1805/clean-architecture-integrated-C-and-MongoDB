using Application.DTOs.Abstract;
using Shared.Enums;

namespace Application.DTOs.AuditLogs
{
    public class AuditLogFilterDto : BasePagingFilterDto
    {
        public string? UserId { get; set; }
    }
}
