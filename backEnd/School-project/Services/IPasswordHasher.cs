namespace Orbita.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPasswordWithSalt, string password);
    }
}
