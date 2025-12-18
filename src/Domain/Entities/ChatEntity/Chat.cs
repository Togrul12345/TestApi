using Domain.Common.Entities;
using Domain.Entities.MessageEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class Chat:BaseAuditedEntity<int>
    {
   
        public string? Avatar { get; set; }
        public string? FoneImg { get; set; }
        public int? SuperAdminId { get; set; }
        public virtual AppUser? SuperAdmin { get; set; }
       
        
        public virtual List<Message> Messages { get; set; }
        public virtual List<ChatUser> ChatUsers { get; set; }
    }
}
