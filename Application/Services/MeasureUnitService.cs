using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class MeasureUnitService : BaseService<MeasureUnit, MeasureUnitDTO, CreateMeasureUnitDTO, UpdateMeasureUnitDTO>, IMeasureUnitService
    {
        protected override IGenericRepository<MeasureUnit> Repository => _unitOfWork.MeasureUnits;
        public MeasureUnitService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider)
            : base(unitOfWork, objectMapper, serviceProvider)
        {

        }
    }
}
