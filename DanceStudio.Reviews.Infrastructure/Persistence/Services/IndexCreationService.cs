using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DanceStudio.Reviews.Infrastructure.Persistence.Services
{

    public class IndexCreationService : IHostedService
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<IndexCreationService> _logger;

        public IndexCreationService(MongoDbContext context, ILogger<IndexCreationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating MongoDB Indexes...");

            var reviews = _context.Reviews;


            var targetIndex = Builders<Review>.IndexKeys.Ascending(r => r.TargetId);
            await reviews.Indexes.CreateOneAsync(new CreateIndexModel<Review>(targetIndex), cancellationToken: cancellationToken);


            var userIndex = Builders<Review>.IndexKeys.Ascending(r => r.Reviewer.UserId);
            await reviews.Indexes.CreateOneAsync(new CreateIndexModel<Review>(userIndex), cancellationToken: cancellationToken);


            var compoundIndex = Builders<Review>.IndexKeys
                .Ascending(r => r.TargetId)
                .Descending(r => r.Rating.Value);
            await reviews.Indexes.CreateOneAsync(new CreateIndexModel<Review>(compoundIndex), cancellationToken: cancellationToken);

            _logger.LogInformation("MongoDB Indexes created successfully.");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}