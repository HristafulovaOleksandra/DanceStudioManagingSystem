using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Domain.Interfaces;
using DanceStudio.Reviews.Infrastructure.Persistence;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DanceStudio.Reviews.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MongoDbContext _context;

        public ReviewRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Review> GetByIdAsync(ObjectId id)
        {
            return await _context.Reviews
                .Find(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.InsertOneAsync(review);
        }

        public async Task UpdateAsync(Review review)
        {
         
            var filter = Builders<Review>.Filter.Eq(r => r.Id, review.Id);
            await _context.Reviews.ReplaceOneAsync(filter, review);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _context.Reviews.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetByTargetAsync(string targetId)
        {
            return await _context.Reviews
                .Find(r => r.TargetId == targetId)
                .SortByDescending(r => r.CreatedAt) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetByReviewerAsync(string userId)
        {
            return await _context.Reviews
                .Find(r => r.Reviewer.UserId == userId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(string targetId)
        {
      
            var pipeline = _context.Reviews.Aggregate()
                .Match(r => r.TargetId == targetId)
                .Group(r => r.TargetId, g => new
                {
                    AverageRating = g.Average(r => r.Rating.Value)
                });

            var result = await pipeline.FirstOrDefaultAsync();
            return result?.AverageRating ?? 0.0;
        }
    }
}