using Application.Dtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class DeleteAppUserFromChatCommand:IRequest<IResult<DomainSuccess<AppUserDto>,DomainError>>
    {
        public int AppUserId { get; set; }
        public int ChatId { get; set; }


        public DeleteAppUserFromChatCommand(int appUserId, int chatId)
        {
            AppUserId = appUserId;
            ChatId = chatId;
        }
    }
}
