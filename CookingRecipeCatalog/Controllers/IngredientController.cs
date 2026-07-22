using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ApiBaseController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _ingredientService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _ingredientService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientDTO ingredientDTO)
        {
            return HandleResult(await _ingredientService.CreateAsync(ingredientDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateIngredientDTO ingredientDTO)
        {
            return HandleResult(await _ingredientService.UpdateAsync(id, ingredientDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _ingredientService.DeleteAsync(id));
        }
    }
}
