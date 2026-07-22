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
    public class IngredientCategoryService : BaseService<IngredientCategory ,IngredientCategoryDTO, CreateIngredientCategoryDTO, UpdateIngredientCategoryDTO>,
        IIngredientCategoryService
    {
        protected override IGenericRepository<IngredientCategory> Repository => _unitOfWork.IngredientCategories;
        public IngredientCategoryService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {
        }

        public override async Task<ServiceResult<IngredientCategoryDTO>> UpdateAsync(int id, UpdateIngredientCategoryDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldCategory = await _unitOfWork.IngredientCategories.GetByIdAsync(id, false);

            if (oldCategory is null)
                return ServiceResult<IngredientCategoryDTO>.Failure("Ingredient category not found", 404);

            if (oldCategory.Name != dto.Name)
            {
                bool HasName = await _unitOfWork.IngredientCategories.HasNameAsync(dto.Name);

                if (HasName)
                    throw new ConflictException("This ingredient category name is already being used");
            }

            var newCategory = _mapper.Map<UpdateIngredientCategoryDTO, IngredientCategory>(dto);
            newCategory.Id = id;

            _unitOfWork.IngredientCategories.Update(newCategory);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<IngredientCategoryDTO>.Success(_mapper.Map<IngredientCategory, IngredientCategoryDTO>(newCategory));
        }
    }
}
