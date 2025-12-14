using Application.Dtos.MessageDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.MessageCommands
{
    public class DeleteMessageCommand:IRequest<IResult<DomainSuccess<ResultMessageDto>,DomainError>>
    {
        public int Id { get; set; }

        public DeleteMessageCommand(int id)
        {
            Id = id;
        }
    }
}
