using Core.Interfaces;
using Core.Interfaces.Repositories;
using Data.Context;
using Data.Repositories;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeCatalogDBContext _context;

        private IRecipeRepository? _recipeRepository;
        private IUserRepository? _userRepository;
        private IIngredientRepository? _ingredientRepository;
        private ICookingStepRepository? _cookingStepRepository;
        private ITipRepository? _tipRepository;
        private IIngredientCategoryRepository? _ingredientCategoryRepository;
        private IRecipeCategoryRepository? _recipeCategoryRepository;
        private IRecipe_CategoryRepository? _recipe_CategoryRepository;
        private IRecipe_IngredientRepository? _recipe_IngredientRepository;
        private IPasswordRepository? _passwordRepository;
        private IMeasureUnitRepository? _measureUnitRepository;
        private IRoleRepository? _roleRepository;

        private bool _disposed;

        public UnitOfWork(RecipeCatalogDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRecipeRepository Recipes => _recipeRepository ??= new RecipeRepository(_context);

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IIngredientRepository Ingredients => _ingredientRepository ??= new IngredientRepository(_context);

        public ICookingStepRepository CookingSteps => _cookingStepRepository ??= new CookingStepRepository(_context);

        public ITipRepository TipRepository => _tipRepository ??= new TipRepository(_context);

        public IRecipeCategoryRepository RecipeCategories => _recipeCategoryRepository ??= new RecipeCategoryRepository(_context);

        public IRecipe_CategoryRepository Recipe_Category => _recipe_CategoryRepository ??= new Recipe_CategoryRepository(_context);

        public IRecipe_IngredientRepository Recipe_Ingredients => _recipe_IngredientRepository ??= new Recipe_IngredientRepository(_context);

        public IIngredientCategoryRepository IngredientCategories => _ingredientCategoryRepository ??= new IngredientCategoryRepository(_context);

        public IPasswordRepository Passwords => _passwordRepository ??= new PasswordRepository(_context);

        public IMeasureUnitRepository MeasureUnits => _measureUnitRepository ??= new MeasureUnitRepository(_context);

        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}