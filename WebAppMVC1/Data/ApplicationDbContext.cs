using DemoClients;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVC1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationUser> OrganisationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}