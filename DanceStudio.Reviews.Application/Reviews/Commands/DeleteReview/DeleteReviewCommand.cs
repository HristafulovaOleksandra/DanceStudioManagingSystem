using DanceStudio.Reviews.Application.Common.Interfaces;

namespace DanceStudio.Reviews.Application.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(string Id) : ICommand;
}