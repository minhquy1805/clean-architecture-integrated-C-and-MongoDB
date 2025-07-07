using Application.DTOs.AuditLogs;
using Domain.Entities;
using MongoDB.Driver;
using Infrastructure.Mongo.Common;

namespace Infrastructure.Mongo.Filters
{
    public static class MongoUserAuditFilterBuilder
    {
        public static FilterDefinition<UserAudit> Build(AuditLogFilterDto filter)
        {
            var filters = new List<FilterDefinition<UserAudit>>();

            filters.AddIfNotNull(x => x.UserId, filter.UserId);

            return filters.CombineFilters();
        }
    }
}
