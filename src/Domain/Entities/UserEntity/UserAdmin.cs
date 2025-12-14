using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class UserAdmin:BaseAuditedEntity<int>
    {
       
        public int ParticipantId { get; set; }
        public virtual AppUser Participant { get; set; }
        public int AdminId { get; set; }
        public virtual AppUser Admin { get; set; }
    }
}
