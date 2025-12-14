using Application.Dtos.ChatDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.ChatQueries
{
    public class GetChatsByUserIdQuery:IRequest<IResult<DomainSuccess<List<ResultChatDto>>,DomainError>>
    {
        public int Id { get; set; }
       

        public GetChatsByUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
