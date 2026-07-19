using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class IngredientService : BaseService<Ingredient, IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>, IIngredientService
    {
        protected override IGenericRepository<Ingredient> Repository => _unitOfWork.Ingredients;
        public IngredientService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider)
            : base(unitOfWork, objectMapper, serviceProvider)
        {

        }
    }
}
