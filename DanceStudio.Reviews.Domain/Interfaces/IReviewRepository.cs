using DanceStudio.Reviews.Domain.Entities;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(ObjectId id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(ObjectId id);

        Task<IEnumerable<Review>> GetByTargetAsync(string targetId);

        Task<IEnumerable<Review>> GetByReviewerAsync(string userId);

        Task<double> GetAverageRatingAsync(string targetId);
    }
}