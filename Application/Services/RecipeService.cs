using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RecipeService : BaseService<Recipe, RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO>, IRecipeService
    {
        protected override IGenericRepository<Recipe> Repository => _unitOfWork.Recipes;

        public RecipeService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IApplicationValidator validator)
             : base(unitOfWork, objectMapper, validator) 
        {
        }

        public async Task<ServiceResult<IEnumerable<RecipeDTO>>> GetAllByQueryAsync (RecipeSearchQuery query)
        {
            var recipes = await _unitOfWork.Recipes.GetAllByQueryAsync(query.Title, query.UserId, query.CategoryIds, query.RequiredIngredientIds,
                query.OptionalIngredientIds, query.ExcludedIngredientIds, query.PageSize, query.PageNumber);

            var dtos = recipes.Select(r => _mapper.Map<Recipe, RecipeDTO>(r));

            return ServiceResult<IEnumerable<RecipeDTO>>.Success(dtos);
        }

        public override async Task<ServiceResult<RecipeDTO>> CreateAsync(CreateRecipeDTO dto)
        {
            await _validator.ValidateAsync(dto);

            if (AnyStepNumberRepeated(dto.CookingSteps))
                return ServiceResult<RecipeDTO>.Failure("Two or more Cooking Steps have the same Step Number", 409);

            var recipe = _mapper.Map<CreateRecipeDTO, Recipe>(dto);

            foreach (var item in recipe.Recipe_Categories)
            {
                item.Category = await _unitOfWork.RecipeCategories.GetByIdAsync(item.CategoryId)
                    ?? throw new NotFoundException(nameof(RecipeCategory), item.CategoryId);
            }

            foreach (var item in recipe.Recipe_Ingredients)
            {
                item.Ingredient = await _unitOfWork.Ingredients.GetByIdAsync(item.IngredientId)
                    ?? throw new NotFoundException(nameof(Ingredient), item.IngredientId);

                item.Unit = await _unitOfWork.MeasureUnits.GetByIdAsync(item.UnitId)
                    ?? throw new NotFoundException(nameof(MeasureUnit), item.UnitId);
            }

            recipe.User = await _unitOfWork.Users.GetByIdAsync(recipe.UserId)
                ?? throw new NotFoundException(nameof(User), recipe.UserId);

            Repository.Add(recipe);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<RecipeDTO>.Success(_mapper.Map<Recipe, RecipeDTO>(recipe), statusCode: 201);
        }

        public override async Task<ServiceResult<RecipeDTO>> UpdateAsync(int id, UpdateRecipeDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldRecipe = await Repository.GetByIdAsync(id, false);

            if (oldRecipe is null)
                return ServiceResult<RecipeDTO>.Failure("Entity not found.", 404);

            if (oldRecipe.Title != dto.Title)
            {
                bool HasTitle = await _unitOfWork.Recipes.HasTitleAsync(dto.Title);
                if (HasTitle) return ServiceResult<RecipeDTO>.Failure("This recipe name is already being used.", 409);
            }

            if (oldRecipe.ImageUrl != dto.ImageUrl)
            {
                bool HasURL = await _unitOfWork.Recipes.HasImageURLAsync(dto.ImageUrl);
                if (HasURL) return ServiceResult<RecipeDTO>.Failure("This recipe image url is already being used.", 409);
            }

            var newRecipe = _mapper.Map<UpdateRecipeDTO, Recipe>(dto);
            newRecipe.Id = id;
            newRecipe.UserId = oldRecipe.UserId;

            Repository.Update(newRecipe);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<RecipeDTO>.Success(_mapper.Map<Recipe, RecipeDTO>(newRecipe));
        }

        public async Task<ServiceResult> UpdateRecipeStepsAsync(int recipeId, ICollection<UpdateRecipeStepDTO> steps)
        {
            if (AnyStepNumberRepeated(steps))
                return ServiceResult.Failure("Two or more Cooking Steps have the same Step Number.", 409);

            var oldSteps = await _unitOfWork.CookingSteps.GetStepsByRecipeIdAsync(recipeId);

            int maxStepNumber = steps.Count;

            var stepsToDelete = oldSteps.Where(s => s.StepNumber > maxStepNumber);
            _unitOfWork.CookingSteps.RemoveRange(stepsToDelete);

            foreach (var newStepDTO in steps)
            {
                var newStep = _mapper.Map<UpdateRecipeStepDTO, CookingStep>(newStepDTO);
                newStep.RecipeId = recipeId;

                if (newStepDTO.StepNumber <= oldSteps.Count())
                    _unitOfWork.CookingSteps.Update(newStep);
                else
                    _unitOfWork.CookingSteps.Add(newStep);
            }

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        private static bool AnyStepNumberRepeated(IEnumerable<CreateRecipeStepDTO> cookingSteps)
        {
            return cookingSteps.DistinctBy(x => x.StepNumber).Count() != cookingSteps.Count();
        }
        private static bool AnyStepNumberRepeated(IEnumerable<UpdateRecipeStepDTO> cookingSteps)
        {
            return cookingSteps.DistinctBy(x => x.StepNumber).Count() != cookingSteps.Count();
        }

        public async Task<ServiceResult> AddRecipeCategoryAsync(int recipeId, int categoryId)
        {
            var recipe = await Repository.GetByIdAsync(recipeId) ??
                throw new NotFoundException(nameof(Recipe), recipeId);

            if (!await _unitOfWork.RecipeCategories.ExistsAsync(categoryId))
                throw new NotFoundException(nameof(RecipeCategory), categoryId);

            if (recipe.Recipe_Categories.Any(rc => rc.CategoryId == categoryId))
                throw new ConflictException("This relation between Recipes and Categories already exists.");

            recipe.Recipe_Categories.Add(new Recipe_Category { RecipeId =  recipeId, CategoryId = categoryId });

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RemoveRecipeCategoryAsync(int recipeId, int categoryId)
        {
            var recipe = await Repository.GetByIdAsync(recipeId) ??
                throw new NotFoundException(nameof(Recipe), recipeId);

            if (!recipe.Recipe_Categories.Any(rc => rc.CategoryId == categoryId))
                throw new NotFoundException(nameof(Recipe_Category), new { recipeId, categoryId });

            var recipe_category = recipe.Recipe_Categories.Where(rc => rc.CategoryId == categoryId).First();
            recipe.Recipe_Categories.Remove(recipe_category);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(statusCode: 204);
        }

        public async Task<ServiceResult> UpdateRecipe_IngredientAsync(int recipeId, UpdateRecipe_IngredientDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldRecipe_ingredient = await _unitOfWork.Recipe_Ingredients.GetByIdAsync(dto.Id, false) ??
                throw new NotFoundException(nameof(Recipe_Ingredient), dto.Id);

            if (oldRecipe_ingredient.RecipeId != recipeId)
                throw new ConflictException("This relation doesn't belong to this recipe.");

            var recipe_ingredient = _mapper.Map<UpdateRecipe_IngredientDTO, Recipe_Ingredient>(dto);
            recipe_ingredient.RecipeId = recipeId;

            _unitOfWork.Recipe_Ingredients.Update(recipe_ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(statusCode: 204);
        }

        public async Task<ServiceResult> AddRecipe_IngredientAsync(int recipeId, CreateRecipe_IngredientDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId, false) ??
                throw new NotFoundException(nameof(Recipe), recipeId);

            if (!await _unitOfWork.Ingredients.ExistsAsync(dto.IngredientId))
                throw new NotFoundException(nameof(Recipe_Ingredient), dto.IngredientId);

            if (recipe.Recipe_Ingredients.Any(ri => ri.IngredientId == dto.IngredientId))
                throw new ConflictException("This relation between Recipes and Ingredients already exists.");

            var recipe_ingredient = _mapper.Map<CreateRecipe_IngredientDTO, Recipe_Ingredient>(dto);
            recipe_ingredient.RecipeId = recipeId;

            _unitOfWork.Recipe_Ingredients.Add(recipe_ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RemoveRecipe_IngredientAsync(int recipeId, int recipe_ingredientId)
        {
            var recipe_ingredient = await _unitOfWork.Recipe_Ingredients.GetByIdAsync(recipe_ingredientId, false) ??
                throw new NotFoundException(nameof(Recipe_Ingredient), recipe_ingredientId);

            _unitOfWork.Recipe_Ingredients.Delete(recipe_ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(statusCode: 204);
        }
    }
}