using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.entities;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ChefsController : ControllerBase
    {
        private readonly IChefService _chefService;

        public ChefsController(IChefService chefService)
        {
            _chefService = chefService;
        }

        [HttpGet]
        public async Task<IActionResult> ChesfList(CancellationToken cancellationToken)
        {
            var values = await _chefService.GetAllAsync(cancellationToken);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef([FromBody] Chef chef, CancellationToken cancellationToken)
        {
            await _chefService.CreateAsync(chef, cancellationToken);
            return Ok("Şef sisteme başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChef(int id, CancellationToken cancellationToken)
        {
            var deleted = await _chefService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("Şef bulunamadı.");
            }

            return Ok("Şef sistemden silindi.");
        }

        [HttpGet("GetChef")]
        public async Task<IActionResult> GetChef(int id, CancellationToken cancellationToken)
        {
            var chef = await _chefService.GetByIdAsync(id, cancellationToken);
            if (chef is null)
            {
                return NotFound("Şef bulunamadı.");
            }

            return Ok(chef);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChef([FromBody] Chef chef, CancellationToken cancellationToken)
        {
            var updated = await _chefService.UpdateAsync(chef, cancellationToken);
            if (!updated)
            {
                return NotFound("Şef bulunamadı.");
            }

            return Ok("Şef güncelleme işlemi başarılı");
        }
    }
}