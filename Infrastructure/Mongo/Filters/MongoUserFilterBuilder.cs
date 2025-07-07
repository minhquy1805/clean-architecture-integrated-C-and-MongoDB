

using Application.DTOs.Users.Filters;
using Domain.Entities;
using MongoDB.Driver;
using Infrastructure.Mongo.Common;

namespace Infrastructure.Mongo.Filters
{
    public static class MongoUserFilterBuilder
    {
        public static FilterDefinition<User> Build(UserFilterDto filter)
        {
            var filters = new List<FilterDefinition<User>>();

            filters.AddIfNotNull(u => u.FullName, filter.FullName, useRegex: true);
            filters.AddIfNotNull(u => u.Email, filter.Email, useRegex: true);
            filters.AddIfNotNull(u => u.Role, filter.Role);
            filters.AddIfNotNull(u => u.Flag, filter.Flag);

            return filters.CombineFilters();
        }
    }
}
