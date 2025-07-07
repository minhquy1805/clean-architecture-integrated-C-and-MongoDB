

using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Base;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Users
{
    public class MongoRefreshTokenRepository : MongoBaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public MongoRefreshTokenRepository(MongoDbContext context)
            : base(context.Database, "RefreshTokens")
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _collection.Find(r => r.Token == token).FirstOrDefaultAsync();
        }

        public async Task DeleteByUserIdAsync(string userId)
        {
            await _collection.DeleteManyAsync(r => r.UserId == userId);
        }

        // Tùy chọn: lấy tất cả token theo UserId (nếu cần)
        public async Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(string userId)
        {
            return await _collection.Find(r => r.UserId == userId).ToListAsync();
        }
    }
}
