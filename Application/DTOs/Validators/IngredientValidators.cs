using Core.Interfaces.Repositories;
using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateIngredientDTOValidator : AbstractValidator<CreateIngredientDTO>
    {
        public CreateIngredientDTOValidator(IIngredientRepository repo)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellation) =>
                {
                    return !await repo.HasNameAsync(name);
                }).WithMessage("This Name already exists.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageURL is required.")
                .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.")
                .MustAsync(async (url, cancellation) =>
                {
                    return !await repo.HasImageURLAsync(url);
                }).WithMessage("This ImageURL already exists.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
        }
    }

    public class UpdateIngredientDTOValidator : AbstractValidator<UpdateIngredientDTO>
    {
        public UpdateIngredientDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageURL is required.")
                .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
        }
    }
}
