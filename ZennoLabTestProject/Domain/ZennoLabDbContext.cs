using Microsoft.EntityFrameworkCore;

namespace ZennoLabTestProject.Domain
{
    public class ZennoLabDbContext : DbContext
    {
        public ZennoLabDbContext(DbContextOptions<ZennoLabDbContext> options) : base(options)
        {
        }

        public DbSet<UserDataSet> UserDataSets { get; set; }
    }
}
