using System.Linq.Expressions;

namespace Application.Interfaces.Common
{
    public interface IMongoBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetOneAsync(Expression<Func<T, bool>> filter);
        Task InsertAsync(T entity);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
    }
}
