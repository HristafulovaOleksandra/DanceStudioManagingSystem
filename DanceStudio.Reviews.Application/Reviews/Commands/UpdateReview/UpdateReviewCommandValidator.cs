using FluentValidation;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                .Must(id => ObjectId.TryParse(id, out _)).WithMessage("Invalid Review ID format.");

            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000);

            RuleFor(v => v.Rating)
                .InclusiveBetween(1, 5);
        }
    }
}