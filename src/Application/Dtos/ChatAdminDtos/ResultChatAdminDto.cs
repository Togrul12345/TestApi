using Application.Common.Mappings;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatAdminDtos
{
    public class ResultChatAdminDto:IMapFrom<ChatAdmin>
    {
        public int ChatId { get; set; }
       
        public int AdminId { get; set; }
      
    }
}
