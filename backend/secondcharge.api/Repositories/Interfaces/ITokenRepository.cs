using Microsoft.AspNetCore.Identity;

namespace secondcharge.api.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, IList<string> roles);
    }
}
