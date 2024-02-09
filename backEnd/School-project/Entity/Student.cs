using Orbita.Entity;
using Orbita.DTO;

namespace Orbita.Entity
{
    public class Student : Entitys
    {    

        public string Name { get; set; }
        public string Email { get; set; }
        public string RA { get; set; }
        public string CPF { get; set; }


        public Student() { }
        public Student(SaveStudentsDTO dto)
        {
            Name = dto.Name;
            Email = dto.Email;
            RA = GenerateRA(dto.RA, dto.Name);
            CPF = dto.CPF;
            
        }

        public string GenerateRA(string ra, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {                 
                string[] nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string lastSurname = nameParts.LastOrDefault();                              
                int hashCode = Math.Abs(lastSurname.GetHashCode() % 1000);
                string result = ra + hashCode.ToString();              
                return result;
            }

            return string.Empty;
        }

        public void ChangeNameOrEmail(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }



    }
}
