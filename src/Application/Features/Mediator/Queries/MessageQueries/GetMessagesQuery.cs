using Application.Dtos.MessageDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.MessageQueries
{
    public class GetMessagesQuery:IRequest<IResult<DomainSuccess<List<ResultMessageDto>>,DomainError>>
    {
        public int ChatId { get; set; }

        public GetMessagesQuery(int chatId)
        {
            ChatId = chatId;
        }
    }
}
