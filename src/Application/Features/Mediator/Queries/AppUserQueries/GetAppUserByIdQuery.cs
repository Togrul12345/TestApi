using Application.Dtos;
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
    public class GetAppUserByIdQuery:IRequest<IResult<DomainSuccess<AppUserDto>,DomainError>>
    {
        public int Id { get; set; }

        public GetAppUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
