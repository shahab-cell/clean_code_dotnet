using CleanCode.Domain.DTO;
using CleanCode.Interface.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CleanCode.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartApplication cartApplication;
        private readonly IHttpClientFactory httpClientFactory;
        public CartController(ICartApplication cartApplication, IHttpClientFactory httpClientFactory)
        {
            this.cartApplication = cartApplication;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost("cart/add/userId/{userId}")]
        public async Task<IActionResult> AddCart(string userId, [FromBody] List<string> products)
        {
            //-- Case handle for parameter
            ArgumentNullException.ThrowIfNull(products);
            if (products.Count < 0)
            {
                return Ok(new { message = "No Products to add to cart", status = "fail" });
            }

            var result = await cartApplication.AddCart(userId, products);

            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpPost("cart/remove/userId/{userId}")]
        public async Task<IActionResult> RemoveCart(string userId, [FromBody] List<string> products)
        {
            //-- Case handle for parameter
            ArgumentNullException.ThrowIfNull(products);
            if (products.Count < 0)
            {
                return Ok(new { message = "No Products to add to cart", status = "fail" });
            }

            var result = await cartApplication.RemoveCart(userId, products);

            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { cart = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpPost("courseId")]
        public async Task<IActionResult> GetUserByCourseId()
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient();

                string apiUrl = "https://easypaisaacademy.pk/auth/learntechpk/user_progress.php";

                // Prepare request data
                var requestData = new
                {
                    action = "userprogress_retrieved",
                    webtoken = "Lteasy@2024%paisa",
                    courseid = "5"
                };
                // Convert data to JSON
                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

                // Create the HTTP request content
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await httpClient.PostAsync(apiUrl, content);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Optionally, deserialize JSON response if needed
                    // var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseObjectType>(jsonResponse);

                    // Return response
                    return Ok(jsonResponse);
                }
                else
                {
                    // Return error status code if request was not successful
                    return StatusCode((int)response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
