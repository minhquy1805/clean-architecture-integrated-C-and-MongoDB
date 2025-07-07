using Application.DTOs.LoginHistories;
using Domain.Entities;
using MongoDB.Driver;
using Infrastructure.Mongo.Common;

namespace Infrastructure.Mongo.Filters
{
    public class MongoLoginHistoryFilterBuilder
    {
        public static FilterDefinition<LoginHistory> Build(LoginHistoryFilterDto filter)
        {
            var filters = new List<FilterDefinition<LoginHistory>>();

            filters.AddIfNotNull(x => x.IpAddress!, filter.IpAddress, useRegex: true);
            filters.AddIfNotNull(x => x.UserAgent!, filter.UserAgent, useRegex: true);
            filters.AddIfNotNull(x => x.Message!, filter.Message, useRegex: true);
            filters.AddIfNotNull(x => x.UserId, filter.UserId);
            filters.AddIfNotNull(x => x.IsSuccess, filter.IsSuccess);


            return filters.CombineFilters();
        }
    }
}
