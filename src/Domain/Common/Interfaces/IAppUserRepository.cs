using Domain.Common.Interfaces;
using Domain.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public interface IAppUserRepository
    {
        Task<IQueryable<AppUser>> GetAllIncluding();
    }
}
