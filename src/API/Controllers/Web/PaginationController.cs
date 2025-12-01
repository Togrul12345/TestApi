using API.Controllers.Base;
using Application.Features.Mediator.Queries.PaginationQueries;
using Domain.Common.Pagionation;
using Domain.Entities.UserEntity;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginationController : BaseApiController
    {
        

        [HttpPost("Get")] // pagnation Get methodu olmalidi [FromQuery olaraq]
        //Get methodu yazin harda ki pageNumber ve pageSize parametrelari olacaq ve proyekti arashdirin 
        //PaginatedList<T> var ve PaginationFilteri ver onlarin komekliyi ile yazin Domain/Common/Pagination icinde var
        //controllerde db connection olmamalidi siz bunu neter ki user ucun get methodlari yazmisiz helede
        //pagination yazilmali idi
        public async Task<IActionResult> Create([FromQuery] int pageNumber,
    [FromQuery] int pageSize)
        {
            
            return Ok(await Mediator.Send(new GetPaginationQuery(pageSize,pageNumber)));
        }
    }
}
