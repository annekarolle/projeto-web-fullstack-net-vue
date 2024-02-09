using Orbita.Entity;

namespace Orbita.Services
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
