using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByQuery([FromQuery] RecipeSearchQuery query)
        {
            return HandleResult(await _recipeService.GetAllByQueryAsync(query));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
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

        [HttpPatch("{id:int}/Steps")]
        public async Task<IActionResult> UpdateSteps(int id, ICollection<UpdateRecipeStepDTO> stepsDTO)
        {
            return HandleResult(await _recipeService.UpdateRecipeStepsAsync(id, stepsDTO));
        }

        [HttpPost("{recipeId:int}/Category/{categoryId:int}")]
        public async Task<IActionResult> UpdateRecipe_Category(int recipeId, int categoryId)
        {
            return HandleResult(await _recipeService.AddRecipeCategoryAsync(recipeId, categoryId));
        }

        [HttpDelete("{recipeId:int}/Category/{categoryId:int}")]
        public async Task<IActionResult> DeleteRecipe_Category(int recipeId, int categoryId)
        {
            return HandleResult(await _recipeService.RemoveRecipeCategoryAsync(recipeId, categoryId));
        }

        [HttpPost("{recipeId:int}/Ingredient")]
        public async Task<IActionResult> AddIngrendient(int recipeId, CreateRecipe_IngredientDTO dto)
        {
            return HandleResult(await _recipeService.AddRecipe_IngredientAsync(recipeId, dto));
        }

        [HttpPut("{recipeId:int}/Ingredient")]
        public async Task<IActionResult> UpdateIngredient(int recipeId, UpdateRecipe_IngredientDTO dto)
        {
            return HandleResult(await _recipeService.UpdateRecipe_IngredientAsync(recipeId, dto));
        }

        [HttpDelete("{recipeId:int}/Ingredient/{recipe_ingredientId:int}")]
        public async Task<IActionResult> DeleteIngredient(int recipeId, int recipe_ingredientId)
        {
            return HandleResult(await _recipeService.RemoveRecipe_IngredientAsync(recipeId, recipe_ingredientId));
        }

    }
}