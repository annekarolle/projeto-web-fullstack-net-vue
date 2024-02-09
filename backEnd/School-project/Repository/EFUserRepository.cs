using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orbita.Entity;
using Orbita.Interface;
using Orbita.Services;
using System.Linq;
 

namespace Orbita.Repository
{
    public class EFUserRepository : EFRepository<User>, IUserRepository
    {
        private readonly PasswordHasherService _passwordHasher;

        public EFUserRepository(ApplicationDbContext context, PasswordHasherService passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
        }

       
        /// <summary>
        /// Faz a verificação das credenciais passadas e retorna o usuário.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User ValidatedCredential(string email, string password)
        {   
            var user = _context.User.FirstOrDefault(user => user.Email == email);

            bool isValidCredentials = _passwordHasher.VerifyPassword(user.Password, password);
            if(isValidCredentials)
            {
              return user;
            }

            return null;
        }


        /// <summary>
        /// Faz a verificação se o email já esta registrado
        /// </summary>
        /// <param name="email"></param>        
        /// <returns></returns>
        public bool IsEmailAlreadyRegistered(string email)
        {
            var user = _context.User.FirstOrDefault(user => user.Email == email);
                         
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }                        
        }

        /// <summary>
        /// Faz a verificação se o email já esta registrado
        /// </summary>
        /// <param name="email"></param>        
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            var user = _context.User.FirstOrDefault(user => user.Email == email);

            return user ?? throw new ArgumentException("Usuário não encontrado!");
        }

    }
}
