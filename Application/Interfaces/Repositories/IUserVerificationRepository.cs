using Application.Interfaces.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserVerificationRepository : IMongoBaseRepository<UserVerification>
    {
        Task<UserVerification?> GetByTokenAsync(string token);
        Task MarkAsUsedAsync(string userVerificationId);
        Task DeleteExpiredAsync();
    }
}
