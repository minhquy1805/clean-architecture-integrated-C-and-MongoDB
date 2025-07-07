
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Common
{
    public static class MongoFilterBuilder
    {
        /// <summary>
        /// ✅ Dùng cho các field không phải string
        /// </summary>
        public static void AddIfNotNull<T, TField>(
            this List<FilterDefinition<T>> filters,
            Expression<Func<T, TField>> field,
            TField? value
        ) where T : class
        {
            if (value == null) return;
            filters.Add(Builders<T>.Filter.Eq(field, value));
        }

        /// <summary>
        /// ✅ Dùng cho các field string, cho phép Regex
        /// </summary>
        public static void AddIfNotNull<T>(
             this List<FilterDefinition<T>> filters,
             Expression<Func<T, string>> field,
             string? value,
             bool useRegex = false
         ) where T : class
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            if (useRegex)
            {
                var fieldDef = new ExpressionFieldDefinition<T, string>(field); // ✅ ép kiểu đúng
                filters.Add(Builders<T>.Filter.Regex(fieldDef, new BsonRegularExpression(value, "i")));
            }
            else
            {
                filters.Add(Builders<T>.Filter.Eq(field, value));
            }
        }

        /// <summary>
        /// ✅ Kết hợp tất cả filters bằng AND
        /// </summary>
        public static FilterDefinition<T> CombineFilters<T>(this List<FilterDefinition<T>> filters)
        {
            return filters.Count > 0
                ? Builders<T>.Filter.And(filters)
                : Builders<T>.Filter.Empty;
        }

        /// <summary>
        /// ✅ Hỗ trợ từ ngày (>=)
        /// </summary>
        public static void AddFromDate<T>(
            this List<FilterDefinition<T>> filters,
            Expression<Func<T, DateTime>> field,
            DateTime? fromDate
        ) where T : class
        {
            if (fromDate.HasValue)
                filters.Add(Builders<T>.Filter.Gte(field, fromDate.Value));
        }

        /// <summary>
        /// ✅ Hỗ trợ đến ngày (<=)
        /// </summary>
        public static void AddToDate<T>(
            this List<FilterDefinition<T>> filters,
            Expression<Func<T, DateTime>> field,
            DateTime? toDate
        ) where T : class
        {
            if (toDate.HasValue)
                filters.Add(Builders<T>.Filter.Lte(field, toDate.Value));
        }

        /// <summary>
        /// ✅ Lọc theo danh sách (IN)
        /// </summary>
        public static void AddIn<T, TField>(
            this List<FilterDefinition<T>> filters,
            Expression<Func<T, TField>> field,
            IEnumerable<TField>? values
        ) where T : class
        {
            if (values != null && values.Any())
                filters.Add(Builders<T>.Filter.In(field, values));
        }
    }
}
