using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MessageEntity
{
    public class MessageDeletedForUser
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }   // Mesajı silən istifadəçi
    
        public virtual Message Message { get; set; }
    }
}
