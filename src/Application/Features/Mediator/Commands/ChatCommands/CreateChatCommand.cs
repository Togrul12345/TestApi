using Application.Dtos.ChatDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ChatCommands
{
    public class CreateChatCommand:IRequest<IResult<DomainSuccess<ResultChatDto>, DomainError>>
    {
        public string Avatar { get; set; }
        public string FoneImg { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
