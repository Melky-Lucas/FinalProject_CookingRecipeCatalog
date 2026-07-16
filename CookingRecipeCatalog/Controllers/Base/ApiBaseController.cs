using Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Base
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ServiceResult<T> result)
        {
            if (result.IsSuccess) return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, result.Data);
        }

        protected IActionResult HandleResult(ServiceResult result)
        {
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
