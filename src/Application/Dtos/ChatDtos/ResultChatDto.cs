using Application.Common.Mappings;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatDtos
{
    public class ResultChatDto:IMapFrom<Chat>
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FoneImg { get; set; }
        public int? SuperAdminId { get; set; }
   
        public int? AdminId { get; set; }
    }
}
