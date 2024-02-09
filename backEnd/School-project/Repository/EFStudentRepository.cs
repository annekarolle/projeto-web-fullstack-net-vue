using Microsoft.AspNetCore.Http.HttpResults;
using orbita.Interface;
using Orbita.Entity;
using Orbita.Interface;
 

namespace Orbita.Repository
{
    public class EFStudentRepository : EFRepository<Student>, IStudentRepository
    {
        public EFStudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Faz a verificação se o email já esta registrado
        /// </summary>
        /// <param name="email"></param>        
        /// <returns></returns>
        public bool IsEmailAlreadyRegistered(string email)
        {
            var user = _context.Student.FirstOrDefault(user => user.Email == email);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Student GetByRA(string ra)
        {
            var student = _context.Student.FirstOrDefault(student => student.RA == ra);

            return student ?? throw new ArgumentException("Aluno não encontrado!");
        }


        public string DeleteStudent(string ra)
        {
            var student = _context.Student.FirstOrDefault(student => student.RA == ra);

            if (student == null)
            {
                 throw new ArgumentException("Aluno não encontrado!");
            }
            _dbSet.Remove(student);
            _context.SaveChanges();

            return $"{student.Name} deletado com sucesso!";
        }
    }
}
