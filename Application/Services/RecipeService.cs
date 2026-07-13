using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RecipeService : BaseService<Recipe, RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO> ,IRecipeService
    {
        protected override IGenericRepository<Recipe> Repository => _unitOfWork.Recipes;

        public RecipeService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider)
             : base(unitOfWork, objectMapper, serviceProvider) 
        {

        }

    }
}