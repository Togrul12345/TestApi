using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class InviteLink:BaseAuditedEntity<int>
    {
        public DateTime ExpiredIn { get; set; }
        public bool IsRevoked { get; set; }
        public virtual Chat Chat { get; set; }
        public int? ChatId { get; set; }
    }
}
