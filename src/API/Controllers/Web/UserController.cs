using API.Controllers.Base;
using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Queries.AppUserQueries;
using Domain.Common.Interfaces;
using Domain.Entities.UserEntity;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController //Burda BaseApiControlleri ishlede bilersiz onda mediatoru burda cagirmaq ehtiyaci olmayacaq
    {
       

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateAppUserCommand command)
        {
            
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Login")]
        public async Task <IActionResult> Login(LoginAppUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAppUsersQuery()));
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetAppUserByIdQuery(id)));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateAppUserCommand command)
        {
            
            
            return Ok(Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            
            return Ok(Mediator.Send(new DeleteAppUserCommand(id)));
        }

    }
}
