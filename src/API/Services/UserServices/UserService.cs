
using Infrastructure.Contexts;

namespace API.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly TestDbContext _context;

        public UserService(TestDbContext context)
        {
            _context = context;
        }

        public async Task AssignStatus(bool status, int userId)
        {
            var user = _context.AppUsers.Find(userId);
            user.IsOnline = status;
            await _context.SaveChangesAsync();
        }
    }
}
