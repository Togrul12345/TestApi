using API.Controllers.Base;
using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Commands.ChatCommands;
using Application.Features.Mediator.Commands.MessageCommands;
using Application.Features.Mediator.Queries.ChatQueries;
using Application.Features.Mediator.Queries.MessageQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ChatsController : BaseApiController
    {
        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetChatsQuery()));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //istifadəçinin chat siyahısı
        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetChatsByUserIdQuery(id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        //chat haqqında məlumat
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetChatByIdQuery(id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //mesajları əldə etmək
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessagesByChatId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetMessagesQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //- chat yaratmaq
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateChatCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //admin təyin etmək
        [HttpPost("{id}/admins/{userId}")]
        public async Task<IActionResult> AssignAdmin(int id, int userId)
        {
            try
            {
                return Ok(await Mediator.Send(new AssignAdminToChatCommand(id, userId)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //- iştirakçı əlavə etmək
        [HttpPost("{id}/members")]
       
        public async Task<IActionResult> AddParticipant(int id, int participantId)
        {
            try
            {
                return Ok(await Mediator.Send(new AddAppUserToChatCommand(id, participantId)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //mesaj göndərmək
        [HttpPost("{id}/messages")]
       
        public async Task<IActionResult> CreateMessage(int id, string content)
        {
            try
            {
                var command = new CreateMessageCommand();
                command.ChatId = id;
                command.Content = content;

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        //chat məlumatlarını yeniləmək
        public async Task<IActionResult> Update(int id, string Avatar
        , string FoneImg)
        {
            try
            {
                var command = new UpdateChatCommand();
                command.Id = id;
                command.Avatar = Avatar;

                command.FoneImg = FoneImg;
                return Ok(await Mediator.Send(command));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //chat-i silmək (super-admin)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChatCommand(id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //iştirakçını silmək
     
        [HttpDelete("{id}/members/{userId}")]
        public async Task<IActionResult> DeleteUserFromChat(int userId, int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteAppUserFromChatCommand(userId, id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //admini silmek
       
        [HttpDelete("{id}/admins/{userId}")]
        public async Task<IActionResult> DeleteAdmin(int id, int userId)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteAdminFromChatCommand(userId, id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        

    }
}

