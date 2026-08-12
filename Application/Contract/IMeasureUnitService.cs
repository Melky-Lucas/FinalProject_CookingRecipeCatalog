using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IMeasureUnitService : IBaseService<MeasureUnitDTO, CreateMeasureUnitDTO, UpdateMeasureUnitDTO>
    {
        Task<ServiceResult<IEnumerable<MeasureUnitDTO>>> AddRangeAsync(IEnumerable<CreateMeasureUnitDTO> measureUnitDTOs);
    }
}