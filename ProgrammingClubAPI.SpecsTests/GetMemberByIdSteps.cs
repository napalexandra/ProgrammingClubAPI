using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Repositories;
using ProgrammingClubAPI.Services;
using TechTalk.SpecFlow;

namespace ProgrammingClubAPI.SpecsTests
{
    [Binding]
    public class GetMemberByIdSteps
    {
        private MembersService _membersService;
        private ProgrammingClubDataContext _context;
        private MembersRepository _membersRepository;
        private Member _testMember;
        private Member _resultMember;


        [Given(@"a member exists with IdMember ""(.*)""")]
        public void GivenAMemberExistWithIdMember(string idMember)
        {
            var id = Guid.Parse(idMember);

            _testMember = new Member()
            {
                IdMember = id,
                Username = "testuser",
                Name = "Test User",
                Title = "Test Title",
                Description = "Test Description",
                Resume = "Test Resume",
                Position = "Test Position",
                Password = "TestPassword123"
            };

            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                 .Options;

            _context = new ProgrammingClubDataContext(options);

            _context.Database.EnsureCreated();

            _context.Members.Add(_testMember);
            _context.SaveChanges();

            _membersRepository = new MembersRepository(_context);
            _membersService = new MembersService(_membersRepository, null);
        }

        [When(@"the member is requested by IdMember")]
        public async Task WhenTheMemberIsRequestedByIdMember()
        {
            _resultMember = await _membersService.GetMemberByIdAsync(_testMember.IdMember);
        }

        [Then(@"the member should be returned with IdMember")]
        public void ThenTheMemberShouldBeReturnedWithIdMember()
        {
            Assert.NotNull(_resultMember);
            Assert.Equal(_testMember.IdMember, _resultMember.IdMember);
            Assert.Equal(_testMember.Username, _resultMember.Username);
            Assert.Equal(_testMember.Name, _resultMember.Name);
            Assert.Equal(_testMember.Title, _resultMember.Title);
            Assert.Equal(_testMember.Description, _resultMember.Description);
            Assert.Equal(_testMember.Resume, _resultMember.Resume);
        }
    }
}