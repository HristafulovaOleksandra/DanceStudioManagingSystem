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

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

            ConfigureBsonMappings();
        }

        public IMongoCollection<Review> Reviews =>
            _database.GetCollection<Review>("reviews");

        private static void ConfigureBsonMappings()
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