using Microsoft.AspNetCore.Identity;

namespace ProgrammingClubAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
