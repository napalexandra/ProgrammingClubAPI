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

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
             return await _membersRepository.GetMemberByIdAsync(id);
        }

        public async Task AddMemberAsync(Member member)
        {
            if(await _membersRepository.UsernameExistsAsync(member.Username))
            {
                throw new ArgumentException("Username already exists.", nameof(member.Username));
            }
            member.IdMember = Guid.NewGuid();
            await _membersRepository.AddMemberAsync(member);
        }
    }
}
