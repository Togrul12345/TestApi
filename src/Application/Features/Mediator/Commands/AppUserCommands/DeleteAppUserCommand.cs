using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class DeleteAppUserCommand:IRequest<IResult<DomainSuccess<int>,DomainError>>
    {
        public int Id { get; set; }

        public DeleteAppUserCommand(int id)
        {
            Id = id;
        }
    }
}
