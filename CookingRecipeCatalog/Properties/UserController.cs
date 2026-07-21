using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await _userService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _userService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO userDTO)
        {
            return HandleResult(await _userService.CreateAsync(userDTO));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateUserDTO userDTO)
        {
            return HandleResult(await _userService.UpdateAsync(id, userDTO));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _userService.DeleteAsync(id));
        }
    }
}

