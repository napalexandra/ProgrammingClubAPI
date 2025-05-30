using AutoMapper;
using ProgrammingClubAPI.Models;
using ProgrammingClubAPI.Models.CreateOrUpdateModels;
using ProgrammingClubAPI.Repositories;

namespace ProgrammingClubAPI.Services
{
    public class MembersService : IMembersService
    {
        private readonly IMembersRepository _membersRepository;
        private readonly IMapper _mapper;
        public MembersService(IMembersRepository repository, IMapper mapper)
        {
            _membersRepository = repository;
            _mapper = mapper;
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
            if (await _membersRepository.UsernameExistsAsync(member.Username))
            {
                throw new ArgumentException("Username already exists.", nameof(member.Username));
            }
            member.IdMember = Guid.NewGuid();
            await _membersRepository.AddMemberAsync(member);
        }

        public async Task<Member> UpdateMemberAsync(Guid id, Member member)
        {
            if (!await _membersRepository.MemberExistsAsync(id))
            {
                return null;

            }
            member.IdMember = id;
            return await _membersRepository.UpdateMemberAsync(member);
        }

        public async Task<Member> UpdateMemberPartiallyAsync(Guid id, UpdateMemberPartially updateMember)
        {
            if (!await _membersRepository.MemberExistsAsync(id))
            {
                return null;
            }

            Member member = _mapper.Map<Member>(updateMember);

            member.IdMember = id;

            return await _membersRepository.UpdateMemberPartiallyAsync(member);
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            if (!await _membersRepository.MemberExistsAsync(id))
            {
                return false;
            }
            return await _membersRepository.DeleteMemberAsync(id);
        }
    }
}
