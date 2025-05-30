using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.Repositories
{
    public interface IMembersRepository
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(Guid id);

        //Task<Member> GetMemberByUsernameAsync(string username);

        Task AddMemberAsync(Member member);

        Task<bool> UsernameExistsAsync(string username);

        Task<Member> UpdateMemberAsync(Member member);

        Task<Member> UpdateMemberPartiallyAsync(Member member);

        Task<bool> MemberExistsAsync(Guid id);

        Task<bool> DeleteMemberAsync(Guid id);
    }
}
