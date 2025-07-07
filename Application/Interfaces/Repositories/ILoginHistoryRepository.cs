using Application.DTOs.LoginHistories;
using Application.Interfaces.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ILoginHistoryRepository : IMongoBaseRepository<LoginHistory>
    {
        /// <summary>
        /// Lấy lịch sử đăng nhập theo UserId
        /// </summary>
        Task<IEnumerable<LoginHistory>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Phân trang + lọc động theo điều kiện
        /// </summary>
        Task<IEnumerable<LoginHistory>> SelectSkipAndTakeWhereDynamicAsync(LoginHistoryFilterDto filter);

        /// <summary>
        /// Đếm số bản ghi sau khi lọc
        /// </summary>
        Task<int> GetRecordCountWhereDynamicAsync(LoginHistoryFilterDto filter);
    }
}
