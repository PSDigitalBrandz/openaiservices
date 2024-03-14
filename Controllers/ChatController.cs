using Microsoft.AspNetCore.Mvc;
using openaiservices.Models;
using openaiservices.Services;

namespace openaiservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly ChatService _service;
        public ChatController(ChatService service)
        {
            _service = service;
        }
        [HttpPost("PostMessage")]
        public async Task<ActionResult> PostMessage([FromBody] List<Message> messages)
        {
            Message response = await _service.GetResponse(messages);
            messages.Add(response);
            return Ok(messages);
        }
    }
}
