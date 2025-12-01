using DanceStudio.Catalog.Bll.DTOs;
using FluentValidation;

namespace DanceStudio.Catalog.Bll.Validators
{
    public class InstructorCreateUpdateValidator : AbstractValidator<InstructorCreateUpdateDTO>
    {
        public InstructorCreateUpdateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}