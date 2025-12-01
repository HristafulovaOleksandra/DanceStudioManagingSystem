using FluentValidation;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {

            RuleFor(v => v.TargetId)
                .NotEmpty().WithMessage("TargetId is required.");

            RuleFor(v => v.TargetType)
                .NotEmpty().WithMessage("TargetType is required.")
                .Must(type => type == "DanceClass" || type == "Instructor")
                .WithMessage("TargetType must be either 'DanceClass' or 'Instructor'.");

            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

            RuleFor(v => v.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("UserName is required.");
        }

    }
}