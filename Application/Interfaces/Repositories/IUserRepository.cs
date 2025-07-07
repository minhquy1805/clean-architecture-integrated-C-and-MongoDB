using Application.DTOs.Users.Filters;
using Application.Interfaces.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository : IMongoBaseRepository<User>
    {
        // ✅ Các hàm mở rộng dành riêng cho User
        Task<User?> GetByEmailAsync(string email);

        Task UpdateIsActiveAsync(string userId, bool isActive); // ✅ Đổi int → string

        Task<IEnumerable<User>> GetDropDownListDataAsync();

        // ✅ Các hàm paging/filter refactor cho Mongo
        Task<IEnumerable<User>> GetAllWhereDynamicAsync(UserFilterDto filter);

        Task<IEnumerable<User>> SelectSkipAndTakeAsync(int start, int rows, string sortBy);

        Task<IEnumerable<User>> SelectSkipAndTakeWhereDynamicAsync(UserFilterDto filter);

        Task<int> GetRecordCountAsync();

        Task<int> GetRecordCountWhereDynamicAsync(UserFilterDto filter);
    }
}
