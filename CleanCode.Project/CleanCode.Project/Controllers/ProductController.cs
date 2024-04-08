using CleanCode.Application.Application;
using CleanCode.Domain.DTO;
using CleanCode.Interface.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication productApplication;
        public ProductController(IProductApplication productApplication) 
        {
            this.productApplication = productApplication;
        }

        [HttpPost("add/product")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            //-- Parameter Null Check
            ArgumentNullException.ThrowIfNull(product);

            var result = await productApplication.AddProduct(product);
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpPost("edit/product")]
        public async Task<IActionResult> EditProduct([FromBody] Product product)
        {
            //-- Parameter Null Check
            ArgumentNullException.ThrowIfNull(product);

            var result = await productApplication.EditProduct(product);
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpGet("products/id/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            //-- Parameter Null Check
            ArgumentNullException.ThrowIfNull(productId);

            var result = await productApplication.GetProductById(productId);
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] string typeCategory, [FromQuery] string animeCategory,
                                                     [FromQuery] int minPrice, [FromQuery] int maxPrice)
        {
            var result = await productApplication.GetProducts(typeCategory, animeCategory, minPrice, maxPrice);
            //-- Exception Check
            if (result.Item2 is Exception exception)
            {
                Console.WriteLine($"An exception occurred: {exception.Message}");
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "fail" });
            }
            //-- Result Check
            if (result.Item1.Count > 0)
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
            else
            {
                return Ok(new { product = result.Item1, exception = result.Item2.Message, status = "success" });
            }
        }
    }
}
