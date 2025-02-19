﻿using CleanCode.Domain.DTO;
using CleanCode.Domain.JWT_Configuration;
using CleanCode.Interface.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication userApplication;
        private readonly JwtConfigurations jwtConfigurations;
        public UserController(IUserApplication userApplication, JwtConfigurations jwtConfigurations)
        {
            this.userApplication = userApplication;
            this.jwtConfigurations = jwtConfigurations;
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            //-- Parameter Null Check
            ArgumentNullException.ThrowIfNull(user);

            var result = await userApplication.RegisterUser(user);
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

        [HttpPost("login/user")]
        public async Task<IActionResult> LoginUser([FromBody] Login user)
        {
            // Check Params
            List<string> requiredFields = [];
            if (string.IsNullOrEmpty(user.Email))
            {
                requiredFields.Add("Email is required");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                requiredFields.Add("Password is required");
            }
            if (requiredFields.Count != 0)
            {
                string message = string.Join(", ", requiredFields);
                return Ok(new { message, status = "fail" });
            }

            var result = await userApplication.LoginUser(user);

            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1 == null)
            {
                return Ok(new { message = "Incorrect Password, Please Try Again", status = "fail" });
            }
            if (result.Item1.Count > 0)
            {
                var token = jwtConfigurations.CreateToken(result.Item1[0]);
                if (token != null)
                {
                    return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success", token });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Ok(new { user = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [Authorize]
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
