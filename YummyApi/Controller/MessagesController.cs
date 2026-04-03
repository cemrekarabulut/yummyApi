using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Dtos.ContactDtos.MessageDto;
using YummyApi.Services;

namespace YummyApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> MessageList(CancellationToken cancellationToken)
        {
            var value = await _messageService.GetAllAsync(cancellationToken);
            return Ok(value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id, CancellationToken cancellationToken)
        {
            var deleted = await _messageService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound("Mesaj bulunamadı.");
            }

            return Ok("Mesaj silme işlemi başarılı.");
        }

        [HttpGet("GetMessage")]
        public async Task<IActionResult> GetMessage(int id, CancellationToken cancellationToken)
        {
            var value = await _messageService.GetByIdAsync(id, cancellationToken);
            if (value is null)
            {
                return NotFound("Mesaj bulunamadı.");
            }

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageDto updateMessageDto, CancellationToken cancellationToken)
        {
            var updated = await _messageService.UpdateAsync(updateMessageDto, cancellationToken);
            if (!updated)
            {
                return NotFound("Mesaj bulunamadı.");
            }

            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}