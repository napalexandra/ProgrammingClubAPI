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

        //Task UpdateMemberAsync(Member member);
        //Task DeleteMemberAsync(Guid id);
        //Task<bool> MemberExistsAsync(Guid id);
        //
    }
}
