using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class CookingStepService : BaseService<CookingStep, RecipeCookingStepDTO, CreateRecipeCookingStepDTO, UpdateCookingStepDTO>, ICookingStepService
    {
        protected override IGenericRepository<CookingStep> Repository => _unitOfWork.CookingSteps;
        public CookingStepService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {
        }
    }
}
