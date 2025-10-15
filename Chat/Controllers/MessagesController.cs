using Application.Interfaces;
using Application.ModelsDTO;
using ChatApi.Hubs.Interfaces;
using Domain.Records;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IChatsHub ChatsHub { get; init; }

        private IMessagesService MessagesService { get; init; }

        public MessagesController(IChatsHub chatsHub, IMessagesService messagesService)
        {
            ChatsHub = chatsHub;
            MessagesService = messagesService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<MessageResponseDTO>> CreateMessage(
            [FromBody] MessageCreateDTO dto)
        {
            MessageResponseDTO result;
            try
            {
                result = await MessagesService.CreateMessageAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            await ChatsHub.SendMessageAsync(result);
            return Ok(result);
        }

        [HttpGet("{chatId:int}")]
        [ProducesResponseType(typeof(KeysetPaginationAfterResult<MessageResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<KeysetPaginationAfterResult<MessageResponseDTO>>> GetMessages(
            [FromQuery] int chatId,
            [FromQuery] string? after,
            [FromQuery] string? propName,
            [FromQuery] int? limit,
            [FromQuery] bool? reverse)
        {
            KeysetPaginationAfterResult<MessageResponseDTO> result;
            try
            {
                result = await MessagesService.GetMessagesKeysetPaginationAsync(chatId, after, propName, limit, reverse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{messageId:int}")]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<MessageResponseDTO>> DeleteMessage(int messageId)
        {
            MessageResponseDTO result;
            try
            {
                result = await MessagesService.DeleteMessageAsync(messageId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            await ChatsHub.DeleteMessageAsync(result);
            return Ok(result);
        }

        [HttpPut("{messageId:int}")]
        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<MessageResponseDTO>> UpdateMessage(
            int messageId,
            [FromBody] MessageUpdateDTO dto)
        {
            MessageResponseDTO result;
            try
            {
                result = await MessagesService.UpdateMessageAsync(messageId, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            await ChatsHub.UpdateMessageAsync(result);
            return Ok(result);
        }


    }
}
