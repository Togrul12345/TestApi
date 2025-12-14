using Application.Common.Mappings;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatUserDtos
{
    public class ResultChatUserDto:IMapFrom<ChatUser>
    {
        public int ChatId { get; set; }
        
        public int ParticipantId { get; set; }
        
    }
}
