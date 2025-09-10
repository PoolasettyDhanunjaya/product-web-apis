// File: Controllers/ProductController.cs

using Microsoft.AspNetCore.Mvc;
using web_apis.Models;
using web_apis.Services;

namespace web_apis.Controllers
{
    // API route: api/product
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Dependency: ProductService for business logic
        private  readonly ProductService _productService;

        // Optional dependency: logger (not yet used)
        private readonly ILogger<ProductController> _logger;

        // Inject ProductService
        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        /// <summary>
        /// POST: Add a new product
        /// </summary>
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null");

            // Save product to DB
            await _productService.CreateAsync(product, typeof(Product).ToString());

            return Ok(new
            {
                Message = "Product saved successfully",
                Product = product
            });
        }

        /// <summary>
        /// POST: Update an existing product
        /// </summary>
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null");

            // Update product or upsert
            await _productService.UpdateAsync(product, typeof(Product).ToString());

            return Ok(new
            {
                Message = "Product saved successfully",
                Product = product
            });
        }

        /// <summary>
        /// GET: Retrieve all products
        /// </summary>
        [HttpGet("GetProducts")]
        public List<Product> GetProducts()
        {
            // Using .Result for simplicity (can be improved with async)
            var res = _productService.GetProductListAsync();
            return res.Result;
        }

        /// <summary>
        /// GET: Retrieve a product by its ID
        /// </summary>
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            var res= await _productService.GetProductAsync(id);
            return Ok(res);
        }

        /// <summary>
        /// GET: Delete a product by its ID (?? note: should ideally be a DELETE verb)
        /// </summary>
        [HttpGet("DeleteById")]
        public async void DeleteByIdGetProduct(string id)
        {
            await _productService.DeleteAsync(id, typeof(Product).ToString());
        }
    }
}
