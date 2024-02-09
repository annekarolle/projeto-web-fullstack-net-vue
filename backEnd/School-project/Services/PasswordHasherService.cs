using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Orbita.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
        }

        public bool VerifyPassword(string hashedPasswordWithSalt, string password)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            string hashedPasswordToVerify = Convert.ToBase64String(
           KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA256,
               iterationCount: 10000,
               numBytesRequested: 256 / 8
             ));

            return storedHash.Equals(hashedPasswordToVerify);
        }
    }
}
