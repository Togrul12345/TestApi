using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class Ban:BaseAuditedEntity<int>
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime Until { get; set; }
        public string Reason { get; set; }
    }
}
