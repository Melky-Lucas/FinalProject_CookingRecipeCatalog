using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IMeasureUnitService : IBaseService<MeasureUnitDTO, CreateMeasureUnitDTO, UpdateMeasureUnitDTO>
    {
    }
}