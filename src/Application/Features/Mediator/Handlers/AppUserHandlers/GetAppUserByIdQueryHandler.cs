using Application.Dtos;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, IResult<DomainSuccess<AppUserDto>, DomainError>>
    {
        private readonly IRepository<AppUser,int> _repository;
        private readonly ILocalizationService _localizationService;

        public GetAppUserByIdQueryHandler(IRepository<AppUser, int> repository, ILocalizationService localizationService)
        {
            _repository = repository;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<AppUserDto>, DomainError>> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetSingleAsync(a => a.Id == request.Id);
            if (value == null)
            {
                return Result.Fail<AppUserDto>(DomainError.NotFound(_localizationService[ResponseMessages.ExampleNotFound]));
            }
            else
            {
                var dto = new AppUserDto
                {
                    Id = value.Id,
                    FirstName = value.FirstName,
                    Gmail = value.Gmail,
                    LastName = value.LastName,
                    PasswordHash = value.PasswordHash,
                    UserName = value.UserName,
                };
                return Result.Success<AppUserDto>(DomainSuccess<AppUserDto>.OK(dto, _localizationService[ResponseMessages.ExampleFoundSuccessfully]));
            }

        //public async Task<GetAppUsersQueryResult> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        //{
        //    var value=await _repository.GetSingleAsync(a=>a.Id==request.Id);


        //    return new GetAppUsersQueryResult
        //    {
        //        Id = value.Id,
        //        FirstName = value.FirstName,
        //        Gmail = value.Gmail,
        //        LastName = value.LastName,
        //        PasswordHash = value.PasswordHash,
        //        UserName = value.UserName,
        //    };
    }
    }
}
