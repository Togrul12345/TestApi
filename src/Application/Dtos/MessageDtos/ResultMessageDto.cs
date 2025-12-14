using Application.Common.Mappings;
using Domain.Entities.ChatEntity;
using Domain.Entities.MessageEntity;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MessageDtos
{
    public class ResultMessageDto:IMapFrom<Message>
    {
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        // Ümumi Əlaqələr
        public int? SenderId { get; set; }
     

        public int? ReceiverId { get; set; }
        
        public int? MessageReplyId { get; set; }
       
        public string? Status { get; set; }
        public string? Reaction { get; set; }
        public string? FilePath { get; set; } // Faylın serverdəki yolu və ya URL
        public string? FileName { get; set; }
        public long? FileSize { get; set; } // Bayt ilə ölçüsü
        public int ChatId { get; set; }
       
    }
}
