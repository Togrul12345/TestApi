using Application.Dtos.MessageDtos;
using Application.Features.Mediator.Queries.PaginationQueries;

using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Pagionation;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PaginationHandlers
{
    public class GetPaginationForMessagesQueryHandler : IRequestHandler<GetPaginationForMessagesQuery, IResult<DomainSuccess<PaginatedList<DateTime>>, DomainError>>
    {
        private readonly IMessageRepository _repository;

        public GetPaginationForMessagesQueryHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult<DomainSuccess<PaginatedList<DateTime>>, DomainError>> Handle(GetPaginationForMessagesQuery request, CancellationToken cancellationToken)
        {
            var histories =await _repository.GetMessagesHistory();
            if(histories == null || !histories.Any())
            {
                return Domain.Common.Results.Result.Fail<PaginatedList<DateTime>>(DomainError.NotFound("No message histories found."));
            }
            var result=await PaginatedList<DateTime>.CreateAsync(histories, request.Filter.PageNumber, request.Filter.PageSize);
            return Result.Success<PaginatedList<DateTime>>(DomainSuccess<PaginatedList<DateTime>>.OK(result, "Message histories retrieved successfully."));
        }
    }
}
