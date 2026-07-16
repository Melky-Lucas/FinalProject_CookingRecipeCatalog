using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RecipeCategorySevice : BaseService<RecipeCategory, RecipeCategoryDTO, CreateRecipeCategoryDTO, UpdateRecipeCategoryDTO>, IRecipeCategoryService
    {
        protected override IGenericRepository<RecipeCategory> Repository => _unitOfWork.RecipeCategories;
        public RecipeCategorySevice(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider)
            : base(unitOfWork, objectMapper, serviceProvider)
        {

        }
    }
}
