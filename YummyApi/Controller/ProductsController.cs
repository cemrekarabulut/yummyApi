using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Dtos.ContactDtos.ProductDtos;
using YummyApi.entities;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _validator;
        private readonly IProductService _productService;

        public ProductsController(IValidator<Product> validator, IProductService productService)
        {
            _validator = validator;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(CancellationToken cancellationToken)
        {
            var values = await _productService.GetAllAsync(cancellationToken);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            await _productService.CreateAsync(product, cancellationToken);
            return Ok("Ürün ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var deleted = await _productService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("Ürün bulunamadı.");
            }

            return Ok("Ürün silme işlemi başarılı.");
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
        {
            var value = await _productService.GetByIdAsync(id, cancellationToken);
            if (value is null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            var updated = await _productService.UpdateAsync(product, cancellationToken);
            if (!updated)
            {
                return NotFound("Ürün bulunamadı.");
            }

            return Ok("Ürün güncelleme işlemi başarılı.");
        }

        [HttpPost("CreateProductWithCategory")]
        public async Task<IActionResult> CreateProducWithCategory([FromBody] CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            await _productService.CreateWithCategoryAsync(createProductDto, cancellationToken);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> GetProductListWithCategory(CancellationToken cancellationToken)
        {
            var value = await _productService.GetProductListWithCategoryAsync(cancellationToken);
            return Ok(value);
        }
    }
}