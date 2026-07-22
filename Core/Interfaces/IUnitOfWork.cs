using Core.Interfaces.Repositories;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository Recipes { get; }
        IUserRepository Users { get; }
        IIngredientRepository Ingredients { get; }
        ICookingStepRepository CookingSteps { get; }
        ITipRepository Tips { get; }
        IRecipeCategoryRepository RecipeCategories { get; }
        IRecipe_IngredientRepository Recipe_Ingredients { get; }
        IIngredientCategoryRepository IngredientCategories { get; }
        IPasswordRepository Passwords { get; }
        IMeasureUnitRepository MeasureUnits { get; }
        IRoleRepository Roles { get; }
        Task<int> SaveChangesAsync();
    }
}
