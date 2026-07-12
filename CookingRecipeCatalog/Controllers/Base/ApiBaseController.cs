using Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Base
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ServiceResult<T> result)
        {
            if (result.IsSuccess) return Ok(result.Data);

            return result.StatusCode switch
            {
                404 => NotFound(result.Message),
                400 => BadRequest(result.Message),
                _ => StatusCode(result.StatusCode, result.Message)
            };
        }

        protected IActionResult HandleResult(ServiceResult result)
        {
            if (result.IsSuccess) return Ok(result.Message);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
