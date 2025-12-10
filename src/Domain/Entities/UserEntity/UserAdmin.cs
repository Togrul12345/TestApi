using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntity
{
    public class UserAdmin
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AdminId { get; set; }
        public AppUser Admin { get; set; }
    }
}
