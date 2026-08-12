namespace Application.Base
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync();
        Task<ServiceResult<TDto>> GetByIdAsync(int id);
        Task<ServiceResult<TDto>> CreateAsync(TCreateDto dto);
        Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto dto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}