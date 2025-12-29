using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace secondcharge.api.Data
{
    public class SecondChargeAuthDbContext: IdentityDbContext
    {
        public SecondChargeAuthDbContext(DbContextOptions<SecondChargeAuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Further customization of the ASP.NET Identity model can be done here
            var readerRoleId = "1060c9f8-1e55-4488-a606-207a05badb40";
            var writerRoleId = "54335552-cb3d-45a3-af1d-547cfdb62c45";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
