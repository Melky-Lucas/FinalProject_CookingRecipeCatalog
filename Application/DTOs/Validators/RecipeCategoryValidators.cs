using Core.Interfaces.Repositories;
using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateRecipeCategoryDTOValidator : AbstractValidator<CreateRecipeCategoryDTO>
    {
        public CreateRecipeCategoryDTOValidator(IRecipeCategoryRepository repo)
        {
            RuleFor(x => x.Name.Trim())
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellation) =>
                {
                    bool HasName = await repo.HasNameAsync(name);
                    return !HasName;
                }).WithMessage("This Name already exists.");

            RuleFor(x => x.Description.Trim())
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }
    }

    public class UpdateRecipeCategoryDTOValidator : AbstractValidator<UpdateRecipeCategoryDTO>
    {
        public UpdateRecipeCategoryDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name.Trim())
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description.Trim())
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }
    }
}
