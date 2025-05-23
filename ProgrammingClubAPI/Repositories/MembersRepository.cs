using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.Repositories
{
    public class MembersRepository : IMembersRepository
    {
        public Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetMemberByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
