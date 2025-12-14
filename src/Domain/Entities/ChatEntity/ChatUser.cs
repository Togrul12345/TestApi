using Domain.Common.Entities;
using Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class ChatUser:BaseAuditedEntity<int>
    {
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public int  ParticipantId { get; set; }
        public virtual AppUser Participant { get; set; }
    }
}
