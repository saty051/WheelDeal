using Microsoft.EntityFrameworkCore;
using WheelDeal.Models;

namespace WheelDeal.AppDBContext
{
    public class WheelDealDbContext: DbContext
    {
        public WheelDealDbContext(DbContextOptions<WheelDealDbContext> options):
            base(options)
        {
            
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}
