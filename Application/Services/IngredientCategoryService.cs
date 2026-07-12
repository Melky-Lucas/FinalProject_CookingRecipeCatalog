using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class IngredientCategoryService : BaseService<IngredientCategory ,IngredientCategoryDTO, CreateIngredientCategoryDTO, UpdateIngredientCategoryDTO>,
        IIngredientCategoryService
    {
        protected override IGenericRepository<IngredientCategory> Repository => _unitOfWork.IngredientCategories;
        public IngredientCategoryService(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
            : base(unitOfWork, objectMapper)
        {
        }
    }
}
