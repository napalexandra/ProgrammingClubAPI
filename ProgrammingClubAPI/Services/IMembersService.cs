using ProgrammingClubAPI.Models;

namespace ProgrammingClubAPI.Services
{
    public interface IMembersService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync(); 
    }
}
