using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Dtos.ContactDtos;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList(CancellationToken cancellationToken)
        {
            var values = await _contactService.GetAllAsync(cancellationToken);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto createContactDto, CancellationToken cancellationToken)
        {
            await _contactService.CreateAsync(createContactDto, cancellationToken);
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id, CancellationToken cancellationToken)
        {
            var deleted = await _contactService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("İletişim kaydı bulunamadı.");
            }

            return Ok("Silme işlemi başarılı.");
        }

        [HttpGet("GetContact")]
        public async Task<IActionResult> GetContact(int id, CancellationToken cancellationToken)
        {
            var value = await _contactService.GetByIdAsync(id, cancellationToken);
            if (value is null)
            {
                return NotFound("İletişim kaydı bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateContactDto updateContactDto, CancellationToken cancellationToken)
        {
            var updated = await _contactService.UpdateAsync(updateContactDto, cancellationToken);
            if (!updated)
            {
                return NotFound("İletişim kaydı bulunamadı.");
            }

            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}