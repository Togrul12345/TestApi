using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserEntity;

namespace Domain.Entities.MessageEntity
{
    public class MessageReaction
    {
       
        public int Id { get; set; }

        
        public int MessageId { get; set; }      // Hansı mesaja aid olduğunu göstərir
        
        public virtual Message Message { get; set; }   // Navigation property

       
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }// Kim reaksiyanı etdi
                                           // Əgər istifadəçi modeliniz ApplicationUser-dirsə:
                                           // public ApplicationUser User { get; set; }


        public string ReactionType { get; set; } // 👍, ❤️, 😂 və s.

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
