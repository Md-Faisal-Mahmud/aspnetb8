
using System.Security.Claims;

namespace FirstDemo.Infrastructure.Securities
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}