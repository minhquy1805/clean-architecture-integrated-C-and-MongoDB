

using Application.DTOs.LoginHistories;
using Application.DTOs.Users.Filters;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Base;
using Infrastructure.Mongo.Filters;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Users
{
    public class MongoLoginHistoryRepository : MongoBaseRepository<LoginHistory>, ILoginHistoryRepository
    {
        public MongoLoginHistoryRepository(MongoDbContext context)
            : base(context.Database, "LoginHistories")
        {
        }

        public async Task<IEnumerable<LoginHistory>> GetByUserIdAsync(string userId)
        {
            return await _collection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<LoginHistory>> SelectSkipAndTakeWhereDynamicAsync(LoginHistoryFilterDto filter)
        {
            var builder = Builders<LoginHistory>.Sort;
            var sort = builder.Descending(x => x.IpAddress); // mặc định
            var filters = MongoLoginHistoryFilterBuilder.Build(filter);

            return await _collection.Find(filters)
                .Sort(sort)
                .Skip(filter.Start)
                .Limit(filter.NumberOfRows)
                .ToListAsync();
        }

        public async Task<int> GetRecordCountWhereDynamicAsync(LoginHistoryFilterDto filter)
        {
            var filters = MongoLoginHistoryFilterBuilder.Build(filter);
            return (int)await _collection.CountDocumentsAsync(filters);
        }

    }
}
