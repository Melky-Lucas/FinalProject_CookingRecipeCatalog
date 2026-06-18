using Core.Models;
using Microsoft.AspNetCore.Mvc;
/*
namespace CookingRecipeCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<RecipeCategory>> Get()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeCategory> Get(int id)
        {
            try
            {
                return _categoryService.GetCategoryById(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(RecipeCategory category)
        {
            _categoryService.Addcategory(category);
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, RecipeCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body.");
            }
            try
            {
                _categoryService.UpdateCategory(category);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
*/