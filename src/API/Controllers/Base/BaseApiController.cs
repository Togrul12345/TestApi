using CSharpFunctionalExtensions;
using Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
