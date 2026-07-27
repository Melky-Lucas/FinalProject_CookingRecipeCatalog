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
    public class MeasureUnitController : ApiBaseController
    {
        private readonly IMeasureUnitService _measureUnitService;

        public MeasureUnitController(IMeasureUnitService measureUnitService)
        {
            _measureUnitService = measureUnitService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _measureUnitService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _measureUnitService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMeasureUnitDTO measureUnitDTO)
        {
            return HandleResult(await _measureUnitService.CreateAsync(measureUnitDTO));
        }

        [HttpPost("range")]
        public async Task<IActionResult> CreateRange(IEnumerable<CreateMeasureUnitDTO> measureUnitDTOs)
        {
            return HandleResult(await _measureUnitService.AddRangeAsync(measureUnitDTOs));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateMeasureUnitDTO measureUnitDTO)
        {
            return HandleResult(await _measureUnitService.UpdateAsync(id, measureUnitDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _measureUnitService.DeleteAsync(id));
        }
    }
}

