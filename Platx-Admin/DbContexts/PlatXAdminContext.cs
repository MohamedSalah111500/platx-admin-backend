using Microsoft.EntityFrameworkCore;
using Platx_Admin.Entities;

namespace Platx_Admin.DbContexts
{
    public class PlatXAdminContext : DbContext
    {

        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanFeature> PlanFeatures { get; set; }

        public PlatXAdminContext(DbContextOptions<PlatXAdminContext> options)
       : base(options)
        {
        }

    }
}
