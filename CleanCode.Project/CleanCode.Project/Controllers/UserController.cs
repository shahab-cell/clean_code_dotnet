using CleanCode.Interface.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication userApplication;
        public UserController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await userApplication.GetAllUser();
            return Ok(new { user = result, status = "success" });
        }
    }
}
