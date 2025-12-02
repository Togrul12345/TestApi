using Application.Features.Mediator.Results.AppUserResults;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.AppUserQueries
{
    public class LoginAppUserQuery:IRequest<IResult<DomainSuccess<LoginAppUserResult>,DomainError>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
