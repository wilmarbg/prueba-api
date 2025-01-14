using BusinessLogic.Product.Interface;
using DataClasses.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Obtiene un producto por su código.
        /// </summary>
        /// <param name="id">El código único del producto.</param>
        /// <returns>El producto con el ID especificado.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un producto por su código", Description = "Devuelve los detalles de un producto según el código proporcionado.")]
        public async Task<ActionResult<ProductResponseDto>> GetById(
             [SwaggerParameter(Description = "El código único del producto.")] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">El detalle del producto a crear.</param>
        /// <returns>El producto creado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo producto", Description = "Recibe los detalles de un producto y lo registra en el sistema.")]
        public async Task<IActionResult> Create(ProductRequestDto productDto)
        {
            var result = await _productService.CreateAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">El código único del producto.</param>
        /// <param name="productDto">Los nuevos valores del producto.</param>
        /// <returns>Un código 204 si la actualización es exitosa.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un producto existente", Description = "Actualiza un producto según el código proporcionado.")]
        public async Task<IActionResult> Update(
            [SwaggerParameter(Description = "El código único del producto.")] int id, ProductRequestDto productDto)
        {
            var result = await _productService.UpdateAsync(id, productDto);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
