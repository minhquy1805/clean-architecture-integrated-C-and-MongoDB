using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Infrastructure.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings) 
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoDatabase Database => _database;

        // Collection cho User
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
