using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCategoryController : ApiBaseController
    {
        private readonly IRecipeCategoryService _recipeCategoryService;

        public RecipeCategoryController(IRecipeCategoryService recipeCategoryService)
        {
            _recipeCategoryService = recipeCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _recipeCategoryService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _recipeCategoryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecipeCategoryDTO recipeCategoryDTO)
        {
            return HandleResult(await _recipeCategoryService.CreateAsync(recipeCategoryDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRecipeCategoryDTO recipeCategoryDTO)
        {
            return HandleResult(await _recipeCategoryService.UpdateAsync(id, recipeCategoryDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _recipeCategoryService.DeleteAsync(id));
        }
    }
}
