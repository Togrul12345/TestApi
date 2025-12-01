using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class RoleUser:BaseAuditedEntity<int>
    {
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public AppRole AppRole { get; set; }
        public int AppRoleId { get; set; }
    }
}
