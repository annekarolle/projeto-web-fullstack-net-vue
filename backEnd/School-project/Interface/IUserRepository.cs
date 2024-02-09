using Orbita.Entity;

namespace Orbita.Interface
{
    public interface IUserRepository : IRepository<User>
    {
     
     /// <summary>
     /// Retorna o usuário encontrado com os parametros passados
     /// </summary>
     /// <param Email="Email"></param>
     /// <param password="password"></param>
     /// <returns></returns>       
        User ValidatedCredential(string email, string password);


        /// <summary>
        /// Retornar um boleano se o email já esta registrado no banco
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailAlreadyRegistered(string email);

        /// <summary>
        /// Retorna um usuario com o email informado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetUserByEmail(string email);
    }
}
