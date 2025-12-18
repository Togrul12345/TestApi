using Domain.Common.Entities;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MessageEntity
{
    public  class Message:BaseAuditedEntity<int>
    {
 
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        // Ümumi Əlaqələr
        public int? SenderId { get; set; }
        public virtual AppUser? Sender { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsRead { get; set; }
        public int? ReceiverId { get; set; }
        public virtual AppUser? Receiver { get; set; }
        public int? MessageReplyId { get; set; }
        public virtual Message? MessageReply { get; set; }
        public string? Status { get; set; }
        public string? Reaction { get; set; }
        public string? FilePath { get; set; } // Faylın serverdəki yolu və ya URL
        public string? FileName { get; set; }
        public long? FileSize { get; set; } // Bayt ilə ölçüsü
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
