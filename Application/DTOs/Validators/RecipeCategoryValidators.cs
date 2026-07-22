using Core.Interfaces.Repositories;
using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateRecipeCategoryDTOValidator : AbstractValidator<CreateRecipeCategoryDTO>
    {
        public CreateRecipeCategoryDTOValidator(IRecipeCategoryRepository repository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellation) =>
                {
                    bool HasName = await repository.HasNameAsync(name);
                    return !HasName;
                }).WithMessage("This Name already exists."); ;

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }
    }

    public class UpdateRecipeCategoryDTOValidator : AbstractValidator<UpdateRecipeCategoryDTO>
    {
        public UpdateRecipeCategoryDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }
    }
}
