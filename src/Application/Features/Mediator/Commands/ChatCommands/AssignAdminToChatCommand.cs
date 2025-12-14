using Application.Dtos.ChatAdminDtos;
using Application.Dtos.ChatDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ChatCommands
{
    public class AssignAdminToChatCommand:IRequest<IResult<DomainSuccess<ResultChatAdminDto>,DomainError>>
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }

        public AssignAdminToChatCommand(int chatId, int userId)
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
