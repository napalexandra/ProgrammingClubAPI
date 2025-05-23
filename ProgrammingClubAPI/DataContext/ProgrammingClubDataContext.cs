using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.DataContext
{
    public class ProgrammingClubDataContext : DbContext
    {
        public ProgrammingClubDataContext(DbContextOptions<ProgrammingClubDataContext> options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CodeSnippet> CodeSnippets { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
    }
}
