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
    public class IngredientCategoryController : ApiBaseController
    {
        private readonly IIngredientCategoryService _ingredientCategoryService;

        public IngredientCategoryController(IIngredientCategoryService ingredientCategoryService)
        {
            _ingredientCategoryService = ingredientCategoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _ingredientCategoryService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _ingredientCategoryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientCategoryDTO ingredientCategoryDTO)
        {
            return HandleResult(await _ingredientCategoryService.CreateAsync(ingredientCategoryDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateIngredientCategoryDTO ingredientCategoryDTO)
        {
            return HandleResult(await _ingredientCategoryService.UpdateAsync(id, ingredientCategoryDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _ingredientCategoryService.DeleteAsync(id));
        }
    }
}
