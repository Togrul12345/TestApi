using Domain.Common.Entities;
using Domain.Entities.UserEntity;

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
        public Chat Chat { get; set; }
        public int ParticipantId { get; set; }
        public AppUser Participant { get; set; }
        public int AdminId { get; set; }
        public AppUser Admin { get; set; }

    }
}
