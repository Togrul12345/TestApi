using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class PollVote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int UserId { get; set; }

        public bool Vote { get; set; } // Yes / No
    }

}
