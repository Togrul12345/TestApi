using Application.Dtos;
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
    public class UpdateAppUserCommand: IRequest<IResult<DomainSuccess<AppUserDto>, DomainError>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Gmail { get; set; }
        public string PasswordHash { get; set; }
    }
}
