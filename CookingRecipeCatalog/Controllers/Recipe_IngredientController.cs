using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Recipe_IngredientController : ApiBaseController
    {
        private readonly IRecipe_IngredientService _recipe_IngredientService;

        public Recipe_IngredientController(IRecipe_IngredientService recipe_IngredientService)
        {
            _recipe_IngredientService = recipe_IngredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _recipe_IngredientService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _recipe_IngredientService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipe_IngredientDTO recipe_IngredientDTO)
        {
            return HandleResult(await _recipe_IngredientService.CreateAsync(recipe_IngredientDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateRecipe_IngredientDTO recipe_IngredientDTO)
        {
            return HandleResult(await _recipe_IngredientService.UpdateAsync(id, recipe_IngredientDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _recipe_IngredientService.DeleteAsync(id));
        }
    }
}

