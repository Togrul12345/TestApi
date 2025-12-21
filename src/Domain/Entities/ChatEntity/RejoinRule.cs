using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class RejoinRule
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Reason { get; set; }
        public DateTime Until { get; set; }
    }
}
