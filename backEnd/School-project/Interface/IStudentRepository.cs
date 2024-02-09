using Orbita.Entity;
using Orbita.Interface;
 

namespace orbita.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {        

        /// <summary>
        /// Retornar um boleano se o email já esta registrado no banco
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailAlreadyRegistered(string email);

        /// <summary>
        /// Retornar o aluno encontrado pelo RA registrado no banco
        /// </summary>
        /// <param name="ra"></param>
        /// <returns></returns>
        Student GetByRA(string ra);

        string DeleteStudent(string ra);
    }
}
