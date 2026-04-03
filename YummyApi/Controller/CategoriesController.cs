using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.entities;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList(CancellationToken cancellationToken)
        {
            var values = await _categoryService.GetAllAsync(cancellationToken);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category, CancellationToken cancellationToken)
        {
            await _categoryService.CreateAsync(category, cancellationToken);
            return Ok("Kategori ekleme işlemi başarılı");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var deleted = await _categoryService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("Kategori bulunamadı.");
            }

            return Ok("Kategori silme başarılı");
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken)
        {
            var value = await _categoryService.GetByIdAsync(id, cancellationToken);
            if (value is null)
            {
                return NotFound("Kategori bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category, CancellationToken cancellationToken)
        {
            var updated = await _categoryService.UpdateAsync(category, cancellationToken);
            if (!updated)
            {
                return NotFound("Kategori bulunamadı.");
            }

            return Ok("Kategori güncelleme işlemi başarılı");
        }
    }
}