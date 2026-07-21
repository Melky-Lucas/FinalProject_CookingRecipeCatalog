using Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Application.Contract;
using FluentValidation;

namespace Application.Services
{
    public class ApplicationValidator : IApplicationValidator
    {
        public readonly IServiceProvider _serviceProvider;

        public ApplicationValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task ValidateAsync<TDTO>(TDTO dto)
        {
            var validator = _serviceProvider.GetService<IValidator<TDTO>>();

            if (validator == null)
                return;

            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.Errors);
            }
        }
    }
}
