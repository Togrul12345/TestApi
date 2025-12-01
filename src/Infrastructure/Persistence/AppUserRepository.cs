using Domain.Entities.UserEntity;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly TestDbContext _context;

        public AppUserRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<AppUser>> GetAllIncluding()
        {
            var users = _context.AppUsers.Include(a => a.RoleUsers).ThenInclude(a => a.AppRole);
            return users;
        }
    }
}
