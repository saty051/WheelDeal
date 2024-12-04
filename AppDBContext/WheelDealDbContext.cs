using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using WheelDeal.Models;

namespace WheelDeal.AppDBContext
{
    public class WheelDealDbContext:IdentityDbContext<IdentityUser>
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
