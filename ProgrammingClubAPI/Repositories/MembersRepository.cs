using Microsoft.EntityFrameworkCore;
using ProgrammingClubAPI.DataContext;
using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.Repositories
{
    public class MembersRepository : IMembersRepository
    {
        private readonly ProgrammingClubDataContext _context;
        public MembersRepository(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public Task<Member> GetMemberByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
