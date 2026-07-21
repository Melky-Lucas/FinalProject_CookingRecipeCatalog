using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RecipeService : BaseService<Recipe, RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO> ,IRecipeService
    {
        protected override IGenericRepository<Recipe> Repository => _unitOfWork.Recipes;

        public RecipeService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
             : base(unitOfWork, objectMapper, serviceProvider, validator) 
        {

        }

    }
}