using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ApiBaseController
    {
        private readonly RecipeService _recipeService;
        private readonly IObjectMapper _mapper;

        public RecipeController(RecipeService recipeService, IObjectMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeDTO>>> GetAll()
        {
            var recipes = await _recipeService.GetAll();

            return Ok(recipes.Select(r => _mapper.Map<Recipe, RecipeDTO>(r)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var recipe = await _recipeService.GetById(id);

                return Ok(_mapper.Map<Recipe, RecipeDTO>(recipe));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRecipeDTO recipeDTO)
        {
            var recipe = _mapper.Map<CreateRecipeDTO, Recipe>(recipeDTO);
            await _recipeService.Add(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateRecipeDTO recipeDTO)
        {
            try
            {
                var recipe = _mapper.Map<UpdateRecipeDTO, Recipe>(recipeDTO);

                if (id != recipe.Id)
                    throw new InvalidOperationException("El ID de la receta no coincide con la URI");

                await _recipeService.Update(recipe);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _recipeService.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}