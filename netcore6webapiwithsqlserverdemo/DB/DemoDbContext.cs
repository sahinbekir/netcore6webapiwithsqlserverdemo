using Microsoft.EntityFrameworkCore;
using netcore6webapiwithsqlserverdemo.Models;

namespace netcore6webapiwithsqlserverdemo.DB
{
    public class DemoDbContext:DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext>o)
            :base(o)
        {

        }
        public DbSet<Personal>? Personals { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Job>? Jobs { get; set; }
    }
}
