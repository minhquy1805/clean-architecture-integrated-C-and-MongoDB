using Application.DTOs.AuditLogs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Base;
using Infrastructure.Mongo.Filters;
using MongoDB.Driver;



namespace Infrastructure.Repositories.Users
{
    public class MongoUserAuditRepository : MongoBaseRepository<UserAudit>, IUserAuditRepository
    {
        public MongoUserAuditRepository(MongoDbContext context)
            : base(context.Database, "UserAudits")
        {
        }

        public async Task<IEnumerable<UserAudit>> GetByUserIdAsync(string userId)
        {
            return await _collection.Find(a => a.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<UserAudit>> SelectSkipAndTakeWhereDynamicAsync(AuditLogFilterDto filter)
        {
            var mongoFilter = MongoUserAuditFilterBuilder.Build(filter);
            var sort = Builders<UserAudit>.Sort.Descending("CreatedAt");

            return await _collection.Find(mongoFilter)
                .Sort(sort)
                .Skip(filter.Start)
                .Limit(filter.NumberOfRows)
                .ToListAsync();
        }

        public async Task<int> GetRecordCountWhereDynamicAsync(AuditLogFilterDto filter)
        {
            var mongoFilter = MongoUserAuditFilterBuilder.Build(filter);
            return (int)await _collection.CountDocumentsAsync(mongoFilter);
        }
    }
}
