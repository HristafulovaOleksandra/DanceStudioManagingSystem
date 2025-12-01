using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Domain.ValueObjects;
using DanceStudio.Reviews.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DanceStudio.Reviews.Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _settings;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);

            ConfigureMappings();
        }

        public IMongoCollection<Review> Reviews =>
            _database.GetCollection<Review>(_settings.ReviewsCollectionName);

        private static void ConfigureMappings()
        {
 
            if (!BsonClassMap.IsClassMapRegistered(typeof(Review)))
            {
                BsonClassMap.RegisterClassMap<Review>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Rating)))
            {
                BsonClassMap.RegisterClassMap<Rating>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Reviewer)))
            {
                BsonClassMap.RegisterClassMap<Reviewer>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }
    }
}