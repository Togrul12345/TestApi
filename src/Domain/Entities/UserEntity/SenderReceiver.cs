using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class SenderReceiver
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }

        public int ConnectionId { get; set; }
        public int UnReadCount { get; set; }
    }
}
