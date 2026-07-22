using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class MeasureUnitService : BaseService<MeasureUnit, MeasureUnitDTO, CreateMeasureUnitDTO, UpdateMeasureUnitDTO>, IMeasureUnitService
    {
        protected override IGenericRepository<MeasureUnit> Repository => _unitOfWork.MeasureUnits;
        public MeasureUnitService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {

        }

        public override async Task<ServiceResult<MeasureUnitDTO>> UpdateAsync(int id, UpdateMeasureUnitDTO dto)
        {
            await _validator.ValidateAsync(dto);

            var oldUnit = await Repository.GetByIdAsync(id);

            if (oldUnit is null)
                return ServiceResult<MeasureUnitDTO>.Failure("Entity not found", 404);

            if (oldUnit.Name != dto.Name)
            {
                bool HasName = await _unitOfWork.MeasureUnits.HasNameAsync(dto.Name);
                return ServiceResult<MeasureUnitDTO>.Failure("This Measure Unit name is already being used");
            }

            if (oldUnit.Abbreviation != dto.Abbreviation)
            {
                bool HasAbb = await _unitOfWork.MeasureUnits.HasAbbAsync(dto.Abbreviation);
                return ServiceResult<MeasureUnitDTO>.Failure("This Measure Unit abbreviation is already being used");
            }

            MeasureUnit newUnit = _mapper.Map<UpdateMeasureUnitDTO, MeasureUnit>(dto);
            newUnit.Id = id;

            Repository.Update(newUnit);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<MeasureUnitDTO>.Success(_mapper.Map<MeasureUnit, MeasureUnitDTO>(newUnit));
        }
    }
}
