using Microsoft.AspNetCore.Identity;

namespace AuhthenticationAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
