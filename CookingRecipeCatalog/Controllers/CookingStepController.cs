using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookingStepController : ApiBaseController
    {
        private readonly ICookingStepService _cookingStepService;

        public CookingStepController(ICookingStepService cookingStepService)
        {
            _cookingStepService = cookingStepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _cookingStepService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _cookingStepService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeCookingStepDTO cookingStepDTO)
        {
            return HandleResult(await _cookingStepService.CreateAsync(cookingStepDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCookingStepDTO cookingStepDTO)
        {
            return HandleResult(await _cookingStepService.UpdateAsync(id, cookingStepDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _cookingStepService.DeleteAsync(id));
        }
    }
}

