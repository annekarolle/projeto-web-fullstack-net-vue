using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orbita.Interface;
using Orbita.DTO;
using Orbita.Entity;
using Orbita.Enums;
using Orbita.Interface;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace orbita.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IStudentRepository _studentRepository;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            IUserRepository userRepository,
            ILogger<StudentController> logger,
            IStudentRepository studentRepository
            )
        {
            _userRepository = userRepository;
            _logger = logger;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="SaveStudentsDTO"></param>
        /// <returns></returns>  
        [Authorize]
        [Authorize(Roles = Permitions.Admin)]
        [HttpPost("saveStudent")]
        public IActionResult SaveStudent(SaveStudentsDTO saveDto)
        {

            string emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            if (string.IsNullOrEmpty(saveDto.Email))
            {
                return BadRequest("Informar um email válido!");
            }
            if (string.IsNullOrEmpty(saveDto.Name))
            {
                return BadRequest("Informar um nome válido!");
            }
            if (!Regex.IsMatch(saveDto.Email, emailRegex))
            {
                return BadRequest("E-mail inválido!");
            }

            if (saveDto.CPF.Length <= 10)
            {
                return BadRequest("CPF deve conter 11 digitos sem caracteres especiais");
            }

            if (_studentRepository.IsEmailAlreadyRegistered(saveDto.Email))
            {
                var errorMessage = $"Error: Email {saveDto.Email} já esta cadastrado.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            var student = new Student(saveDto);

            var students = _studentRepository.GetAll();

            student.Id = students.Count + 1;


           _studentRepository.Save(student);

            var message = $"Aluno {saveDto.Name} registrado com sucesso";
            _logger.LogWarning(message);
            return Ok(message);
        }

        /// <summary>
        /// Obtém todos os alunos, o método necessita de autenticação e permissão de Administrador
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Retonar Sucesso</response>
        /// <response code="401"> Não Autenticado</response>
        /// <response code="403"> Ñão Autorizado</response>
        [Authorize]       
        [HttpGet("getAllStudent")]
        public IActionResult GetAllStudents()
        {
            return Ok(_studentRepository.GetAll());
        }

        /// <summary>
        ///  Obtém  alunos por RA, o método necessita de autenticação e permissão de Administrador
        /// </summary>
        /// <param name="ra"></param>
        /// <returns></returns>
        /// <response code="200"> Retonar Sucesso</response>
        /// <response code="401"> Não Autenticado</response>
        /// <response code="403"> Ñão Autorizado</response>
        /// <response code="404"> Aluno não encontrado</response>
        [Authorize]        
        [HttpGet("getStudentByRA/{ra}")]
        public IActionResult GetUserByRA(string ra)
        {
            var user = _studentRepository.GetByRA(ra);

            if (user == null)
            {
                _logger.LogError("Aluno não encontrado!");
                return NotFound("Aluno não encontrado!");
            }
            else
            {
                return Ok(user);
            }                

           
        }


        /// <summary>
        /// Deleta um aluno, o método necessita de permissão de Administrador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Retonar Sucesso</response>
        /// <response code="401"> Não Autenticado</response>
        /// <response code="403"> Ñão Autorizado</response>
        /// <response code="404"> Usuário não encontrado</response>
        [Authorize]
        [Authorize(Roles = Permitions.Admin)]
        [HttpDelete("deleteStudent/{ra}")]
        public IActionResult DeleteStudentRa(string ra)
        {
            var mensagem = _studentRepository.DeleteStudent(ra);           

            _logger.LogInformation(mensagem);

            return Ok(mensagem);
             
        }

        /// <summary>
        /// Modifica email do usuário, método necessita de autenticação.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Retonar Sucesso</response>
        /// <response code="401"> Não Autenticado</response> 
        /// <response code="404"> Usuário não encontrado</response>
        [Authorize]
        [HttpPut("editStudent")]
        public IActionResult EditStudent(PutStudentDTO dto)
        {
            if(dto.Email.Length <= 0 )
            {
                 return BadRequest("Informar um email válido!");
            }
            if (dto.Name.Length <= 0)
            {
                return BadRequest("Informar um nome válido!");
            }
            var student = _studentRepository.GetByRA(dto.RA);

            var email = _studentRepository.IsEmailAlreadyRegistered(dto.Email); 

            if(email != null)
            {
                BadRequest("Email já está cadastrado");
            }         
            
            student.ChangeNameOrEmail(dto.Name, dto.Email);
            _studentRepository.Put(student);  

            return Ok("Dados alterados com sucesso");

        }


    }
}