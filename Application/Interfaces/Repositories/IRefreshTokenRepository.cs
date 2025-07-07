using Application.Interfaces.Common;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IRefreshTokenRepository : IMongoBaseRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task DeleteByUserIdAsync(string userId);
    }
}
