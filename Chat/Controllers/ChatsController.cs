using ChatApi.Hubs.Interfaces;
using Application.Interfaces;
using Application.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private IChatsService ChatsService { get; init; }

        public ChatsController(IChatsService chatsService)
        {
            ChatsService = chatsService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ChatResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDTO>> CreateChat(
            [FromBody]ChatCreateDTO dto)
        {
            ChatResponseDTO result;
            try
            {
                result = await ChatsService.CreateChatAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChatResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChatResponseDTO>>> GetAvailableChatsAsync()
        {
            IEnumerable<ChatResponseDTO> result;
            try
            {
                result = await ChatsService.GetAvailableChatsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("chatId:int")]
        [ProducesResponseType(typeof(ChatResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDTO>> GetChat(int chatId)
        {
            ChatResponseDTO result;
            try
            {
                result = await ChatsService.GetChatAsync(chatId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{chatId:int}")]
        [ProducesResponseType(typeof(ChatResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDTO>> DeleteChat(int chatId)
        {
            ChatResponseDTO result;
            try
            {
                result = await ChatsService.DeleteChatAsync(chatId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}
