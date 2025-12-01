using Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class AppUser:BaseAuditedEntity<int>
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Gmail { get; set; }
        public string PasswordHash { get; set; }
        public List<RoleUser> RoleUsers { get; set; }
    }
}
