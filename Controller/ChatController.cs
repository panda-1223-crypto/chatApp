using System.Text;
using System.Text.Json;
using ChatApp.Entity;
using ChatApp.Model;
using ChatApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ChatController : ControllerBase
    {
        private readonly ChatLogic _chatLogic;
        private readonly ChatRepository _chatRepository;

        public ChatController(ChatLogic chatLogic, ChatRepository chatRepository)
        {
            _chatLogic = chatLogic;
            _chatRepository = chatRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatRequest request)
        {
            var allMessageInfo = await _chatRepository.GetAllAsync();
            var reply = await _chatLogic.CheckMessage(request.Message, allMessageInfo);
            return Ok(new { reply });
        }
    }
}