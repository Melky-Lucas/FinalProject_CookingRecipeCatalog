using Core.Interfaces.Repositories;
using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateRecipeDTOValidator : AbstractValidator<CreateRecipeDTO>
    {
        public CreateRecipeDTOValidator(IRecipeRepository repo)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.")
                .MustAsync(async (title, cancellation) =>
                {
                    return !await repo.HasTitleAsync(title);
                }).WithMessage("This Title already exists.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Desription is required.")
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageURL is required.")
                .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.")
                .MustAsync(async (url, cancellation) =>
                {
                    return !await repo.HasImageURLAsync(url);
                }).WithMessage("This ImageURL already exists.");

            RuleFor(x => x.PreparationTime)
                .GreaterThan(TimeSpan.Zero).WithMessage("Preparation time must be greater than zero.");

            RuleFor(x => x.CookingTime)
                .GreaterThan(TimeSpan.Zero).WithMessage("Cooking time must be greater than zero.");

            RuleFor(x => x.Servings)
                .GreaterThan(0).WithMessage("Servings must be greater than zero.");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("Calories must be greater than or equal to zero.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than zero.");

            RuleFor(x => x.Category_Ids)
                .NotNull().WithMessage("Category IDs cannot be null.")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Category IDs must be greater than zero.");

            RuleFor(x => x.Recipe_Ingredients)
                .NotNull().WithMessage("Recipe ingredients cannot be null.")
                .NotEmpty().WithMessage("Recipe ingredients cannot be empty.");

            RuleForEach(x => x.Recipe_Ingredients)
                .SetValidator(new CreateRecipe_IngredientDTOValidator());

            RuleFor(x => x.CookingSteps)
                .NotNull().WithMessage("Cooking steps cannot be null.")
                .NotEmpty().WithMessage("Cooking steps cannot be empty.");

            RuleForEach(x => x.CookingSteps)
                .SetValidator(new CreateCookingStepDTOValidator());

            RuleForEach(x => x.Tips)
                .NotNull().WithMessage("Tips cannot be null.")
                .NotEmpty().WithMessage("Tips cannot be empty.");

            RuleForEach(x => x.Tips)
                .SetValidator(new CreateTipDTOValidator());
        }

        public class UpdateRecipeDTOValidator : AbstractValidator<UpdateRecipeDTO>
        {
            public UpdateRecipeDTOValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Id must be greater than zero.");

                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title is required.")
                    .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                    .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Desription is required.")
                    .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

                RuleFor(x => x.ImageUrl)
                    .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.");

                RuleFor(x => x.PreparationTime)
                    .GreaterThan(TimeSpan.Zero).WithMessage("Preparation time must be greater than zero.");

                RuleFor(x => x.CookingTime)
                    .GreaterThan(TimeSpan.Zero).WithMessage("Cooking time must be greater than zero.");

                RuleFor(x => x.Servings)
                    .GreaterThan(0).WithMessage("Servings must be greater than zero.");

                RuleFor(x => x.Calories)
                    .GreaterThanOrEqualTo(0).WithMessage("Calories must be greater than or equal to zero.");
            }

        }
    }
}
