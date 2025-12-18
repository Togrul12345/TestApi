using API.Controllers.Base;
using Application.Features.Mediator.Commands.MessageCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class MessagesController : BaseApiController
    {//mesajı redaktə etmək
        [Authorize(Roles = "Owner,Member")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string Content,int chatId)
        {
            try
            {
                var command = new UpdateMessageCommand();
                command.Id = id;
                command.Content = Content;
                command.ChatId = chatId;
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //mesajı silmək
        [Authorize(Roles = "Owner,Member")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteMessageCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //reaksiya əlavə etmək
        [Authorize(Roles = "Owner,Member")]
        [HttpPost("{id}/reaction")]
        public async Task<IActionResult> AddReaction(int id, string reaction)
        {
            try
            {
                var command = new AddReactionToMessageCommand(reaction,id);
                
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //fayl yükləmək
        [Authorize(Roles = "Owner,Member")]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> Upload(IFormFile file,string content,int chatId)
        {
            try
            {
                var command = new UploadFileCommand();
                command.file = file;
                command.Content = content;
                command.ChatId = chatId;
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
