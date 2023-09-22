using DemoClients;
using Microsoft.EntityFrameworkCore;

namespace WebAppApi1.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationUser> OrganisationUsers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {

        }

    }
}
