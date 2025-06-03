using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.UnitTests.Models.Builders;

namespace ProgrammingClubAPI.UnitTests.Helpers
{
    public class DBContextHelper
    {
        public static ProgrammingClubDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .Options;

           
            var context = new ProgrammingClubDataContext(options);

            context.Database.EnsureCreated(); // Create the database if it doesn't exist

            return context;
        }

        public static async Task<Member> AddTestMember(ProgrammingClubDataContext context, Member? testMember = null)
        {
            testMember ??= new MemberBuilder().Build(); // Use the MemberBuilder to create a test member if none is provided

            context.Members.Add(testMember);

            await context.SaveChangesAsync();

            context.Entry(testMember).State = EntityState.Detached; // Detach the entity to avoid tracking issues in tests EF Core sa nu mai urmareasca entitatea Member - sa nu mai tracking.

            return testMember;
        }
    }
}
