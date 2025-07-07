using Application.Interfaces.Common;
using System.Linq.Expressions;
using System.Text.Json;

namespace Application.Services.Common
{
    public abstract class BaseService<TDto, TEntity> : IBaseService<TDto> where TEntity : class
    {
        protected readonly IMongoBaseRepository<TEntity> _repository;

        protected BaseService(IMongoBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto);
        }

        public virtual async Task<TDto?> GetByIdAsync(string id)
        {
            var filter = BuildIdFilter(id);
            var entity = await _repository.GetOneAsync(filter);
            return entity == null ? default : MapToDto(entity);
        }

        public virtual async Task<string> InsertAsync(TDto dto)
        {
            var entity = MapToEntity(dto);
            await _repository.InsertAsync(entity);

            return GetDtoId(dto); // 👈 bạn phải đảm bảo GetDtoId(dto) trả về Id đã gán sau insert
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            await ValidateBeforeUpdate(dto);
            var id = GetDtoId(dto);
            var filter = BuildIdFilter(id);
            var entity = MapToEntity(dto);
            await _repository.UpdateAsync(filter, entity);
        }
        public virtual async Task DeleteAsync(string id)
        {
            await ValidateBeforeDelete(id);
            var filter = BuildIdFilter(id);
            await _repository.DeleteAsync(filter);
        }

        // 🧩 Hook method
        protected virtual Task ValidateBeforeUpdate(TDto dto) => Task.CompletedTask;
        protected virtual Task ValidateBeforeDelete(string id) => Task.CompletedTask;

        // 🧩 Audit Hook: override ở lớp con để ghi log
        protected virtual Task LogAuditAsync(string userId, string action, string? oldValue, string? newValue)
            => Task.CompletedTask;

        // 🧩 Mapping logic (abstract)
        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TDto dto);
        protected abstract string GetDtoId(TDto dto);


        // Abstract mapping logic
        // 🧩 Helper để build filter theo Id
        protected virtual Expression<Func<TEntity, bool>> BuildIdFilter(string id)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(param, GetIdFieldName());
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(equal, param);
        }

        // 🧩 Subclass override nếu field Id không phải là "UserId"
        protected virtual string GetIdFieldName() => "UserId";

        // 🧩 Serialize hỗ trợ audit
        protected string SerializeForAudit(object obj) =>
            JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

       
    }
}
