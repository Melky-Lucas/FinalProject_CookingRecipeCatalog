using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class TipService : BaseService<Tip, RecipeTipDTO, CreateRecipeTipDTO, UpdateTipDTO>, ITipService
    {
        protected override IGenericRepository<Tip> Repository => _unitOfWork.Tips;
        public TipService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IApplicationValidator validator)
           : base(unitOfWork, objectMapper, serviceProvider, validator)
        {

        }
    }
}
