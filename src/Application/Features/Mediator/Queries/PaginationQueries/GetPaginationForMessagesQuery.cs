using Domain.Common.Entities;
using Domain.Common.Pagionation;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.PaginationQueries
{
    public class GetPaginationForMessagesQuery : IRequest<IResult<DomainSuccess<PaginatedList<DateTime>>,DomainError>>
    {
        public PaginationFilter Filter { get; set; }
        public GetPaginationForMessagesQuery(int pageSize, int PageNumber)
        {
            Filter = new PaginationFilter(PageNumber, pageSize);
        }
    }
}
