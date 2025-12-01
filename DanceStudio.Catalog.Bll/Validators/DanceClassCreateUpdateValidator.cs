using DanceStudio.Catalog.Bll.DTOs;
using FluentValidation;

namespace DanceStudio.Catalog.Bll.Validators
{
    public class DanceClassCreateUpdateValidator : AbstractValidator<DanceClassCreateUpdateDTO>
    {
        public DanceClassCreateUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Class name is required.")
                .MaximumLength(100);

            RuleFor(x => x.DifficultyLevel)
                .NotEmpty()
                .Must(level => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(level))
                .WithMessage("Difficulty level must be Beginner, Intermediate, or Advanced.");

            RuleFor(x => x.DefaultPrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}