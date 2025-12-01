using Domain.Entities.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.RoleUserCommands
{
    public class RoleUserCommand:IRequest
    {
        public int AppUserId { get; set; }

        public int AppRoleId { get; set; }
    }
}
