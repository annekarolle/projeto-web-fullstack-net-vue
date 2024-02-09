using Microsoft.AspNetCore.Identity;
using Orbita.DTO;
using Orbita.Enums;
using Orbita.Services;

namespace Orbita.Entity
{
    /// <summary>
    /// Representa um usuário no sistema, contendo informações essenciais e métodos relacionados. Herda propriedade Id de Entitys
    /// </summary>
    public class User : Entitys
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PermitionsTypes Permitions { get; set; }




        public User() { }

        /// <summary>
        /// Cria uma nova instância da classe User com as informações fornecidas, pelo SaveUserDTO
        /// </summary>
        /// <param name="userName">O nome do usuário.</param>
        /// <param name="email">O endereço de e-mail do usuário.</param>
        /// <param name="password">A senha do usuário.</param>
        /// <param Permitions="permitions">Tipo de permissão do usuário no sistema.</param>
        public User(SaveUserDTO saveUserDTO, PasswordHasherService passwordHasher)
        {
            Name = saveUserDTO.Name;
            Email = saveUserDTO.Email;
            Password = passwordHasher.HashPassword(saveUserDTO.Password);

            switch (saveUserDTO.Permitions)
            {
                case PermitionsTypes.Admin:
                    Permitions = PermitionsTypes.Admin;
                    break;
                case PermitionsTypes.Director:
                    Permitions = PermitionsTypes.Director;
                    break;
                default:
                    Permitions = PermitionsTypes.General;
                    break;
            }

        }



        public void ChangeUserPassword(string NewPassword, PasswordHasherService passwordHasher)
        {
            Password = passwordHasher.HashPassword(NewPassword);

        }
    }
}
