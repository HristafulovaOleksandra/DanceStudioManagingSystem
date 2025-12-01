using DanceStudio.Reviews.Application.Common.Interfaces;
using DanceStudio.Reviews.Application.DTOs; 
namespace DanceStudio.Reviews.Application.Reviews.Commands.CreateReview
{

    public record CreateReviewCommand(
        string TargetId,
        string TargetType,
        string Content,
        int Rating,
        string UserId,
        string UserName,
        string? AvatarUrl
    ) : ICommand<string>;
}