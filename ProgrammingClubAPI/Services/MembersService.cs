using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Repositories;

namespace ProgrammingClubAPI.Services
{
    public class MembersService : IMembersService
    {
        private readonly IMembersRepository _membersRepository;
        public MembersService(IMembersRepository repository)
        {
            _membersRepository = repository;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _membersRepository.GetAllMembersAsync();
        }
    }
}
