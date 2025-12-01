using DanceStudio.Reviews.Application.Common.Interfaces;

namespace DanceStudio.Reviews.Application.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(string Id, string Content, int Rating) : ICommand;
}