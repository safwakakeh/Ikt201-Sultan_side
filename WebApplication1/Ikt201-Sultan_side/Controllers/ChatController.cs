using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ikt201_Sultan_side.Services;

namespace Sultan_AI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly GeminiChatService _chatService;

        public ChatController(GeminiChatService chatService)
        {
            _chatService = chatService;
        }

        public class ChatRequest
        {
            public string Message { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
                return BadRequest(new { error = "Message is required" });

            var reply = await _chatService.AskAsync(request.Message);
            return Ok(new { reply });
        }
    }
}