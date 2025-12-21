using Domain.Common.Entities;
using Domain.Entities.ChatEntity;
using Domain.Entities.MessageEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class AppUser:BaseAuditedEntity<int>
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Gmail { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<RoleUser> RoleUsers { get; set; }
      
        public virtual List<UserAdmin> AdminUserAdmins { get; set; }
        public virtual List<UserAdmin> ParticipantUserAdmins { get; set; }
        public virtual List<Message> SentMessages { get; set; }
        public virtual List<Message> ReceivedMessages { get; set; }
        public virtual List<Chat> ChatsOfSuperAdmin { get; set; }
        
        public virtual List<ChatUser> ChatUsers { get; set; }
    }
}
