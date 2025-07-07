namespace Application.Interfaces.Common
{
    public interface IBaseService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(string id);         // 👈 Sửa từ int → string
        Task<string> InsertAsync(TDto dto);          // 👈 Trả về string nếu dùng _id Mongo
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(string id);                 // 👈 Sửa từ int → string
    }
}

