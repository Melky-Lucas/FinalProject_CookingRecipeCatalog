using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Application.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RecipeCategoryService : BaseService<RecipeCategory, RecipeCategoryDTO, CreateRecipeCategoryDTO, UpdateRecipeCategoryDTO>, IRecipeCategoryService
    {
        protected override IGenericRepository<RecipeCategory> Repository => _unitOfWork.RecipeCategories;
        public RecipeCategoryService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {

        }

        public override async Task<ServiceResult<RecipeCategoryDTO>> UpdateAsync(int id, UpdateRecipeCategoryDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldCategory = await _unitOfWork.RecipeCategories.GetByIdAsync(id, false);

            if (oldCategory is null)
                return ServiceResult<RecipeCategoryDTO>.Failure("Entity not found", 404);

            if (oldCategory.Name != dto.Name)
            {
                bool HasName = await _unitOfWork.RecipeCategories.HasNameAsync(dto.Name);

                if (HasName) 
                    throw new ConflictException("This recipe name is already being used");
            }

            var entity = _mapper.Map<UpdateRecipeCategoryDTO, RecipeCategory>(dto);
            entity.Id = id;

            Repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<RecipeCategoryDTO>.Success(_mapper.Map<RecipeCategory, RecipeCategoryDTO>(entity));
        }
    }
}
