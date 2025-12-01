using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    //public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    //{
    //    public TestDbContext CreateDbContext(string[] args)
    //    {
    //        var builder = new DbContextOptionsBuilder<TestDbContext>();

    //        // BURAYA REAL CONNECTION STRING YAZ
    //        builder.UseSqlServer(
    //            "Server=DESKTOP-MFDQNKF\\SQLEXPRESS;Database=TestDb;Trusted_Connection=true;TrustServerCertificate=true");

    //        // mediator dizayn vaxtında istifadə olunmur
    //        return new TestDbContext(builder.Options, null!);
    //    }
    //}
}
