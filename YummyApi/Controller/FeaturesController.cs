using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Dtos.ContactDtos.FeatureDtos;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList(CancellationToken cancellationToken)
        {
            var values = await _featureService.GetAllAsync(cancellationToken);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature([FromBody] CreateFeatureDto createFeatureDto, CancellationToken cancellationToken)
        {
            await _featureService.CreateAsync(createFeatureDto, cancellationToken);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(int id, CancellationToken cancellationToken)
        {
            var deleted = await _featureService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("Özellik bulunamadı.");
            }

            return Ok("Silme işlemi başarılı.");
        }

        [HttpGet("GetFeature")]
        public async Task<IActionResult> GetFeature(int id, CancellationToken cancellationToken)
        {
            var value = await _featureService.GetByIdAsync(id, cancellationToken);
            if (value is null)
            {
                return NotFound("Özellik bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature([FromBody] UpdateFeatureDto updateFeatureDto, CancellationToken cancellationToken)
        {
            var updated = await _featureService.UpdateAsync(updateFeatureDto, cancellationToken);
            if (!updated)
            {
                return NotFound("Özellik bulunamadı.");
            }

            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}