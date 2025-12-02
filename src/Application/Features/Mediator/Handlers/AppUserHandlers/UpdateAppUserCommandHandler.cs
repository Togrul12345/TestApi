using Application.Dtos;
using Application.Features.Mediator.Commands.AppUserCommands;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Constants;
using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, IResult<DomainSuccess<AppUserDto>, DomainError>>
    {
        private readonly IRepository<AppUser, int> _repository;
        private readonly ILocalizationService _localizationService;

        public UpdateAppUserCommandHandler(IRepository<AppUser, int> repository, ILocalizationService localizationService)
        {
            _repository = repository;
            _localizationService = localizationService;
        }

        public async Task<IResult<DomainSuccess<AppUserDto>, DomainError>> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetAllIncluding(u => u.Id == request.Id).FirstOrDefault();
            if (user == null)
            {
                return Result.Fail<AppUserDto>(DomainError.NotFound(_localizationService[ResponseMessages.ExampleNotFound]));
            }
           
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Gmail = request.Gmail;
            user.PasswordHash = request.PasswordHash;
            await _repository.UpdateAsync(user);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success(DomainSuccess<AppUserDto>.OK(new AppUserDto
            {
                Id=user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Gmail = user.Gmail,
                PasswordHash = user.PasswordHash
            }, _localizationService[ResponseMessages.ExampleUpdatedSuccessfully]));
        }

        //public async Task Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        //{

        //   await _repository.UpdateAsync(new AppUser
        //    {
        //        Id = request.Id,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        UserName = request.UserName,
        //        Gmail = request.Gmail,
        //        PasswordHash = request.PasswordHash,
        //    });
        //    await _repository.CommitAsync(cancellationToken);
        //}
    }
}
