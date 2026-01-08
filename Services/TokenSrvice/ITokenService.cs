using Evento.Models;

namespace Evento.Services.TokenSrvice
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, IList<string> roles);
    }
}