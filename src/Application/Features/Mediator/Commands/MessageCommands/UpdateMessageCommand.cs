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
    public class UpdateMessageCommand:IRequest<IResult<DomainSuccess<ResultMessageDto>,DomainError>>
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        // Ümumi Əlaqələr

        public int ChatId { get; set; }
    }
}
