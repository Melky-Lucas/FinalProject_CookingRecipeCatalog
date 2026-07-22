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
        public IngredientService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {

        }

        public override async Task<ServiceResult<IngredientDTO>> UpdateAsync(int id, UpdateIngredientDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldIngredient = await _unitOfWork.Ingredients.GetByIdAsync(id, false); 

            if (oldIngredient is null)
                return ServiceResult<IngredientDTO>.Failure("Ingredient not found", 404);

            if (oldIngredient.Name != dto.Name)
            {
                bool HasName = await _unitOfWork.Ingredients.HasNameAsync(dto.Name);
                if (HasName) return ServiceResult<IngredientDTO>.Failure("This ingredient name is already being used", 409);
            }

            if (oldIngredient.ImageUrl != dto.ImageUrl)
            {
                bool HasURL = await _unitOfWork.Ingredients.HasImageURLAsync(dto.ImageUrl);
                if (HasURL) return ServiceResult<IngredientDTO>.Failure("This ingredient image url is already being used", 409);
            }

            Ingredient newIngredient = _mapper.Map<UpdateIngredientDTO, Ingredient>(dto);
            newIngredient.Id = id;

            Repository.Update(newIngredient);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<IngredientDTO>.Success(_mapper.Map<Ingredient, IngredientDTO>(newIngredient));
        }
    }
}
