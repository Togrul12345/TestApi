using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class RemovedUserFromChat:BaseAuditedEntity<int>
    {
        public int UserId { get; set; }
        public DateTime Until { get; set; }
    }
}
