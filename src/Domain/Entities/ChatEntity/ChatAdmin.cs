using Domain.Common.Entities;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class ChatAdmin:BaseAuditedEntity<int>
    {
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public int AdminId { get; set; }
        public virtual AppUser Admin { get; set; }
    }
}
