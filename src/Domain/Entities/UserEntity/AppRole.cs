using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class AppRole:BaseAuditedEntity<int>
    {
        public string RoleName { get; set; }
        public List<RoleUser> RoleUsers { get; set; }
    }
}
