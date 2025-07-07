using Application.Common.Helpers;
using Application.DTOs.Abstract;
using Application.Interfaces.Abstract;
using Application.Interfaces.Common;
using Shared.Helpers;
using System.Linq.Expressions;
using System.Text.Json;

namespace Application.Services.Abstract
{
    public abstract class BasePagingFilterService<TDto, TEntity, TFilter>
        : IBasePagingFilterService<TDto, TFilter>
        where TEntity : class
        where TFilter : BasePagingFilterDto
    {
        protected readonly IMongoBaseRepository<TEntity> _repository;

        protected BasePagingFilterService(IMongoBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        // 🔹 CRUD cơ bản
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto);
        }

        public virtual async Task<TDto?> GetByIdAsync(string id)
        {
            var entity = await _repository.GetOneAsync(BuildIdFilter(id));
            return entity == null ? default : MapToDto(entity);
        }

        public virtual async Task<string> InsertAsync(TDto dto)
        {
            var entity = MapToEntity(dto);
            await _repository.InsertAsync(entity);
            return GetDtoIntId(dto);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            await ValidateBeforeUpdate(dto);
            var entity = MapToEntity(dto);
            await _repository.UpdateAsync(BuildIdFilter(GetDtoIntId(dto)), entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await ValidateBeforeDelete(id);
            await _repository.DeleteAsync(BuildIdFilter(id));
        }

        // 🔹 Filter & Paging
        public abstract Task<IEnumerable<TDto>> SelectSkipAndTakeWhereDynamicAsync(TFilter filter);
        public abstract Task<int> GetRecordCountWhereDynamicAsync(TFilter filter);

        public async Task<(IEnumerable<TDto> Data, int TotalRecords)> GetPagingAsync(TFilter filter)
        {
            filter.SortBy = SortFieldValidator.Validate(filter.SortBy, GetAllowedSortFields(), "CreatedAt");
            filter.SortDirection = SortFieldValidator.ValidateDirection(filter.SortDirection);

            var totalRecords = await GetRecordCountWhereDynamicAsync(filter);
            var (start, _) = PaginationHelper.GetStartRowIndexAndTotalPages(filter.CurrentPage, filter.NumberOfRows, totalRecords);
            filter.Start = start;

            var data = await SelectSkipAndTakeWhereDynamicAsync(filter);
            return (data, totalRecords);
        }

        // 🔹 Hook methods
        protected virtual Task ValidateBeforeUpdate(TDto dto) => Task.CompletedTask;
        protected virtual Task ValidateBeforeDelete(string id) => Task.CompletedTask;

        protected virtual Task LogAuditAsync(string userId, string action, string? oldValue, string? newValue)
        {
            return Task.CompletedTask;
        }

        // 🔹 Mapping logic
        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TDto dto);
        protected abstract string GetDtoIntId(TDto dto);

        protected abstract string[] GetAllowedSortFields();

        // 🔹 Utility: Convert int Id to Expression for MongoDB
        private static Expression<Func<TEntity, bool>> BuildIdFilter(string id)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(param, "UserId"); // hoặc "Id", tùy entity
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(equal, param);
        }

        protected string SerializeForAudit(object obj) =>
            JsonSerializer.Serialize(obj, _auditOptions);

        protected string SerializePasswordForAudit(string label, string hash) =>
            $"{label}: {hash}";

        private static readonly JsonSerializerOptions _auditOptions = new()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };
    }
}
