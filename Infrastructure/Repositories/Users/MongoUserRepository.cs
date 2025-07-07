using Application.DTOs.Users.Filters;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Base;
using Infrastructure.Mongo.Filters;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Users
{
    public class MongoUserRepository : MongoBaseRepository<User>, IUserRepository
    {
        public MongoUserRepository(MongoDbContext context)
           : base(context.Database, "Users")
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await GetOneAsync(u => u.Email == email);
        }

        public async Task UpdateIsActiveAsync(string userId, bool isActive)
        {
            var update = Builders<User>.Update.Set(u => u.IsActive, isActive);
            await _collection.UpdateOneAsync(u => u.UserId == userId, update);
        }

        public async Task<IEnumerable<User>> GetDropDownListDataAsync()
        {
            // Ví dụ: chỉ lấy ID và FullName
            var projection = Builders<User>.Projection.Include(u => u.UserId).Include(u => u.FullName);
            var results = await _collection.Find(_ => true).Project<User>(projection).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<User>> GetAllWhereDynamicAsync(UserFilterDto filter)
        {
            var query = BuildFilter(filter);
            return await _collection.Find(query).ToListAsync();
        }

        public async Task<IEnumerable<User>> SelectSkipAndTakeAsync(int start, int rows, string sortBy)
        {
            var sort = Builders<User>.Sort.Ascending(sortBy);
            return await _collection.Find(_ => true).Sort(sort).Skip(start).Limit(rows).ToListAsync();
        }

        public async Task<IEnumerable<User>> SelectSkipAndTakeWhereDynamicAsync(UserFilterDto filter)
        {
            var query = BuildFilter(filter);
            var sort = Builders<User>.Sort.Descending("CreatedAt"); // default sort
            return await _collection.Find(query)
                .Sort(sort)
                .Skip(filter.Start)
                .Limit(filter.NumberOfRows)
                .ToListAsync();
        }

        public async Task<int> GetRecordCountAsync()
        {
            return (int)await _collection.CountDocumentsAsync(_ => true);
        }

        public async Task<int> GetRecordCountWhereDynamicAsync(UserFilterDto filter)
        {
            var query = BuildFilter(filter);
            return (int)await _collection.CountDocumentsAsync(query);
        }

        // ✅ Helper: Build dynamic Mongo filter from UserFilterDto
        private FilterDefinition<User> BuildFilter(UserFilterDto filter)
        {
            return MongoUserFilterBuilder.Build(filter);
        }
    }
}
