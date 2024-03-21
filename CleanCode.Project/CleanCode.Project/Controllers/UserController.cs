using CleanCode.Domain.DTO;
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

        [HttpPost("user")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            //-- Parameter Null Check
            ArgumentNullException.ThrowIfNull(user);

            var result = await userApplication.PostUser(user);
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await userApplication.GetAllUser();
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }
    }
}
