using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class Recipe_IngredientService : BaseService<Recipe_Ingredient, Recipe_IngredientDTO, CreateRecipe_IngredientDTO, UpdateRecipe_IngredientDTO>, IRecipe_IngredientService
    {
        protected override IGenericRepository<Recipe_Ingredient> Repository => _unitOfWork.Recipe_Ingredients;
        public Recipe_IngredientService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, validator)
        {
        }
    }
}
