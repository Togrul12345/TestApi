using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options) { }
}
