namespace Application.Contract
{
    public interface IApplicationValidator
    {
        Task ValidateAsync<TDTO>(TDTO dto);
    }
}
