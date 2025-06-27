using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuhthenticationAPI.DataContext
{
    public class ProgrammingClubAuthDataContext : IdentityDbContext
    {
        public ProgrammingClubAuthDataContext(DbContextOptions<ProgrammingClubAuthDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminId = "a49f9034-8be5-4bff-884b-506d0fa9bb28";
            var memberId = "b49f9034-8be5-4bff-884b-506d0fa9bb28";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = memberId,
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

