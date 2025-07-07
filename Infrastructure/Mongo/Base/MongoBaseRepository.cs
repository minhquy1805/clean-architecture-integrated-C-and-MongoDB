

using Application.Interfaces.Common;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Mongo.Base
{
    public abstract class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        protected MongoBaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task UpdateAsync(Expression<Func<T, bool>> filter, T entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }
    }
}
