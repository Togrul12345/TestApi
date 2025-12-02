using API.Controllers.Base;
using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Queries.PaginationQueries;
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
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            
        }
        [HttpPost("Login")]
        public async Task <IActionResult> Login(LoginAppUserQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAppUsersQuery()));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAppUserByIdQuery(id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateAppUserCommand command)
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
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteAppUserCommand(id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
         
        }
        [HttpGet("GetPagination")]
        public async Task<IActionResult> Create([FromQuery] int pageNumber,
    [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPaginationQuery(pageSize, pageNumber)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

    }
}
