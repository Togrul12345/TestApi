using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MessageEntity
{
    public abstract class Message
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        // Ümumi Əlaqələr
        public int SenderId { get; set; }
        public virtual AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public virtual AppUser Receiver { get; set; }
        public Chat Chat { get; set; }
    }
}
