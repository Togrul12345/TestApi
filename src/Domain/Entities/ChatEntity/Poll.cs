using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ChatEntity
{
    public class Poll
    {
        public int Id { get; set; }
        public int ChatId { get; set; }

        public string Question { get; set; } = null!;
        public DateTime EndsAt { get; set; }

        public bool IsClosed { get; set; }
        public bool DecisionLocked { get; set; } // 🔒 QƏRAR QƏTİDİR
    }

}
