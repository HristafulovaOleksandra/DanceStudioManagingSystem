using DanceStudio.Reviews.Application.Common.Interfaces;
using DanceStudio.Reviews.Application.DTOs;

namespace DanceStudio.Reviews.Application.Reviews.Queries.GetReviewsByTarget
{

    public record GetReviewsByTargetQuery(string TargetId) : IQuery<IEnumerable<ReviewDto>>;

}