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
    public class DeleteChatCommand:IRequest<IResult<DomainSuccess<ResultChatDto>,DomainError>>
    {
        public int Id { get; set; }

        public DeleteChatCommand(int id)
        {
            Id = id;
        }
    }
}
