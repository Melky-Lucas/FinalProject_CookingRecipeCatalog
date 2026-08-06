using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ApiBaseController
    {
        private readonly ITipService _tipService;

        public TipController(ITipService tipService)
        {
            _tipService = tipService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _tipService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _tipService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeTipDTO tipDTO)
        {
            return HandleResult(await _tipService.CreateAsync(tipDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTipDTO tipDTO)
        {
            return HandleResult(await _tipService.UpdateAsync(id, tipDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _tipService.DeleteAsync(id));
        }
    }
}

