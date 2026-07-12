using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class TipService : BaseService<Tip, RecipeTipDTO, CreateTipDTO, UpdateTipDTO>, ITipService
    {
        protected override IGenericRepository<Tip> Repository => _unitOfWork.Tips;
        public TipService(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
           : base(unitOfWork, objectMapper)
        {

        }
    }
}
