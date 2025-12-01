using Application.Features.Mediator.Commands.RoleUserCommands;
using Domain.Common.Interfaces;
using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.RoleUserHandlers
{
    public class RoleUserCommandsHandler : IRequestHandler<RoleUserCommand>
    {
        private readonly IRepository<RoleUser, int> _repository;
        public async Task Handle(RoleUserCommand request, CancellationToken cancellationToken)
        {
          await  _repository.AddAsync(new RoleUser
            {
                AppUserId = request.AppUserId,
                AppRoleId = request.AppRoleId
            });
        }
    }
}
