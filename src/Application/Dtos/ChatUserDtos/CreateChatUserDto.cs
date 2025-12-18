using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatUserDtos
{
    public class CreateChatUserDto
    {
        public int ChatId { get; set; }
        
        public int ParticipantId { get; set; }
      
    }
}
