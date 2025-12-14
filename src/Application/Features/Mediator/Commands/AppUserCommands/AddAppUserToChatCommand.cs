using Application.Dtos;
using Application.Dtos.ChatUserDtos;
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
    public class AddAppUserToChatCommand:IRequest<IResult<DomainSuccess<ResultChatUserDto>,DomainError>>
    {
        public int ChatId { get; set; }
        public int AppUserId { get; set; }

        public AddAppUserToChatCommand(int chatId, int appUserId)
        {
            ChatId = chatId;
            AppUserId = appUserId;
        }

    
    }
}
