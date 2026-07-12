using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class CookingStepService : BaseService<CookingStep, RecipeCookingStepDTO, CreateCookingStepDTO, UpdateCookingStepDTO>, ICookingStepService
    {
        protected override IGenericRepository<CookingStep> Repository => _unitOfWork.CookingSteps;
        public CookingStepService(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
            : base(unitOfWork, objectMapper)
        {
        }
    {
    }
}
