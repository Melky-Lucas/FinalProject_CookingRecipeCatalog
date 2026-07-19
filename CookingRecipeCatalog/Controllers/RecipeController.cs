using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ApiBaseController
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _recipeService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _recipeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRecipeDTO recipeDTO)
        {
            return HandleResult(await _recipeService.CreateAsync(recipeDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateRecipeDTO recipeDTO)
        {
            return HandleResult(await _recipeService.UpdateAsync(id, recipeDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _recipeService.DeleteAsync(id));
        }
    }
}