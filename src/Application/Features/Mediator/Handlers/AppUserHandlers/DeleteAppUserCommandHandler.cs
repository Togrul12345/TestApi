using Application.Dtos;
using Application.Features.Mediator.Commands.AppUserCommands;
using Domain.Common.Entities;
using Domain.Common.Interfaces;
using Domain.Common.Results;
using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, Domain.Common.Results.IResult<DomainSuccess<int>, DomainError>>
    {
        private readonly IRepository<AppUser, int> _repository;

        public DeleteAppUserCommandHandler(IRepository<AppUser, int> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<DomainSuccess<int>, DomainError>> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetSingleAsync(a => a.Id == request.Id);
            if (user == null)
            {
                return Result.Fail<int>(DomainError.NotFound($"User with Id {request.Id} not found."));
            }
            await _repository.DeleteAsync(user);
            await _repository.CommitAsync(cancellationToken);
            return Result.Success(DomainSuccess<int>.OK(request.Id, "Deleted"));
        }
    }
}
