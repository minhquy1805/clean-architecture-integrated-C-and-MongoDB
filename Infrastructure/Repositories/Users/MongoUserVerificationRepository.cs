

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Base;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Users
{
    public class MongoUserVerificationRepository : MongoBaseRepository<UserVerification>, IUserVerificationRepository
    {
        public MongoUserVerificationRepository(MongoDbContext context)
           : base(context.Database, "UserVerifications")
        {
        }

        public async Task<UserVerification?> GetByTokenAsync(string token)
        {
            return await _collection.Find(x => x.Token == token).FirstOrDefaultAsync();
        }

        public async Task MarkAsUsedAsync(string userVerificationId)
        {
            var update = Builders<UserVerification>.Update
                .Set(x => x.IsUsed, true)
                .Set(x => x.CreatedAt, DateTime.UtcNow);

            await _collection.UpdateOneAsync(x => x.UserVerificationId == userVerificationId, update);
        }

        public async Task DeleteExpiredAsync()
        {
            var now = DateTime.UtcNow;
            var filter = Builders<UserVerification>.Filter.Lt(x => x.ExpiryDate, now);
            await _collection.DeleteManyAsync(filter);
        }
    }
}
