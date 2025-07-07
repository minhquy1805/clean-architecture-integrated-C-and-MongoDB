using Application.DTOs.AuditLogs;
using Application.Interfaces.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserAuditRepository : IMongoBaseRepository<UserAudit>
    {
        /// <summary>
        /// Lấy toàn bộ lịch sử audit theo UserId
        /// </summary>
        Task<IEnumerable<UserAudit>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Phân trang và lọc động theo điều kiện (search/filter)
        /// </summary>
        Task<IEnumerable<UserAudit>> SelectSkipAndTakeWhereDynamicAsync(AuditLogFilterDto filter);

        /// <summary>
        /// Đếm số lượng bản ghi theo điều kiện động
        /// </summary>
        Task<int> GetRecordCountWhereDynamicAsync(AuditLogFilterDto filter);
    }
}
