using Application.Dtos;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Entities.UserEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUsersQueryHandler : IRequestHandler<GetAppUsersQuery, IResult<DomainSuccess<List<AppUserDto>>,DomainError>>
    { private readonly IRepository<AppUser,int> _repository;

        public GetAppUsersQueryHandler(Domain.Common.Interfaces.IRepository<AppUser,int> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<DomainSuccess<List<AppUserDto>>, DomainError>> Handle(GetAppUsersQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetAll();
            var dtos = values.Select(a => new AppUserDto
            {
                Id = a.Id,
                UserName = a.UserName,
                Gmail = a.Gmail,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PasswordHash = a.PasswordHash,
            }).ToList();
            return Result.Success<List<AppUserDto>>(DomainSuccess<List<AppUserDto>>.OK(dtos, "Found"));

        }

        //public Task<List<GetAppUsersQueryResult>> Handle(GetAppUsersQuery request, CancellationToken cancellationToken)
        //{
        //    var values = _repository.GetAll();
        //    return values.Select(a => new GetAppUsersQueryResult
        //    {
        //        Id = a.Id,
        //        UserName = a.UserName,
        //        Gmail = a.Gmail,
        //        FirstName = a.FirstName,
        //        LastName = a.LastName,
        //        PasswordHash = a.PasswordHash,
        //        RoleUsers=a.RoleUsers,

        //    }).ToListAsync(cancellationToken);
        //}
    }
}
