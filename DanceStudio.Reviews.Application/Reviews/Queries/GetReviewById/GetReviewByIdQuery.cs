using DanceStudio.Reviews.Application.Common.Interfaces;
using DanceStudio.Reviews.Application.DTOs;

namespace DanceStudio.Reviews.Application.Reviews.Queries.GetReviewById
{
    public record GetReviewByIdQuery(string Id) : IQuery<ReviewDto>;
}