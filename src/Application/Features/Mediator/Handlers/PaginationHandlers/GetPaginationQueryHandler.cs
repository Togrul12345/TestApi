using Application.Dtos;
using Application.Features.Mediator.Queries.PaginationQueries;
using AutoMapper;
using CSharpFunctionalExtensions;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Pagionation;
using Domain.Common.Results;
using Domain.Entities.UserEntity;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PaginationHandlers
{
    public class GetPaginationQueryHandler : IRequestHandler<GetPaginationQuery, Domain.Common.Results.IResult<DomainSuccess<PaginatedList<AppUserDto>>, DomainError>>
    {
        private readonly IAppUserRepository _repository;
        private readonly IMapper _mapper;


        public GetPaginationQueryHandler(IAppUserRepository repository, PaginatedList<AppUser> paginatedList, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Domain.Common.Results.IResult<DomainSuccess<PaginatedList<AppUserDto>>, DomainError>> Handle(GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var users =await _repository.GetAllIncluding();
            if (users == null)
            {
                return Domain.Common.Results.Result.Fail<PaginatedList<AppUserDto>>(DomainError.NotFound("Users Not found"));
            }
          var result= await PaginatedList<AppUser>.CreateAsync(users, request.Filter.PageNumber, request.Filter.PageSize);
            var mappedUsers = _mapper.Map<PaginatedList<AppUserDto>>(result);
            return  Domain.Common.Results.Result.Success<PaginatedList<AppUserDto>>(DomainSuccess<PaginatedList<AppUserDto>>.Created(mappedUsers,"Created Successfully" ));

        }
    }
}
