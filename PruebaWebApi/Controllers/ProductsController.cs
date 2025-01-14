using BusinessLogic.Product.Interface;
using DataClasses.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestDto productDto)
        {
            var result = await _productService.CreateAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductRequestDto productDto)
        {
            var result = await _productService.UpdateAsync(id, productDto);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
