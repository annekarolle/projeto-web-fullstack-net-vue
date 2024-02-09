using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Orbita.DTO;
using Orbita.Entity;
using Orbita.Enums;
using Orbita.Interface;
using Orbita.Services;
using System.Security.Claims;
 

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository; 
    private readonly ILogger<UserController> _logger;
    private readonly PasswordHasherService _passwordHasher;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger, PasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    

    /// <summary>
    /// Cria um novo usuário.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns></returns>     

    [HttpPost("saveUser")]
    public IActionResult SaveUser(SaveUserDTO userDTO)
    {
        if (_userRepository.IsEmailAlreadyRegistered(userDTO.Email))
        {
            var errorMessage = $"Error: Email {userDTO.Email} já esta cadastrado.";
            _logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }
        var user = new User(userDTO, _passwordHasher);
        _userRepository.Save(user);
       
        var message = $"User {userDTO.Name} created with sucess!";
        _logger.LogWarning(message);

        return Ok(message);
    }

    /// <summary>
    /// Obtém todos os usuários, o método necessita de autenticação e permissão de Administrador
    /// </summary>
    /// <returns></returns>
    /// <response code="200"> Retonar Sucesso</response>
    /// <response code="401"> Não Autenticado</response>
    /// <response code="403"> Ñão Autorizado</response>
    [Authorize]
    [Authorize(Roles = Permitions.Admin)]
    [HttpGet("getAllUser")]
    public IActionResult GetAllUser()
    {
        return Ok(_userRepository.GetAll());
    }

     


    /// <summary>
    ///  Obtém todos os usuários, o método necessita de autenticação e permissão de Administrador
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"> Retonar Sucesso</response>
    /// <response code="401"> Não Autenticado</response>
    /// <response code="403"> Ñão Autorizado</response>
    /// <response code="404"> Usuário não encontrado</response>
    [Authorize]
    [Authorize(Roles = Permitions.Admin)]
    [HttpGet("getUserById/{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userRepository.GetById(id);

        if(user == null) 
            return NotFound("Usuário não encontrado!");

        return Ok(user);
    }


    /// <summary>
    /// Modifica email do usuário, método necessita de autenticação.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200"> Retonar Sucesso</response>
    /// <response code="401"> Não Autenticado</response> 
    /// <response code="404"> Usuário não encontrado</response>
    [Authorize]
    [HttpPatch("changeUserEmail")]
    public IActionResult ChangeUserEmail(string newEmail)
    {
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        var user = _userRepository.GetUserByEmail(userEmail);

        if (user == null)
        {
            return NotFound("Usuário não encontrado!");
        }
        else
        {                        
            user.Email = newEmail;            
            _userRepository.Put(user);

            return Ok("Usuario alterado com sucesso");
        }               

      
    }

    /// <summary>
    /// Modifica a senha do usuário, método necessita de autenticação.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200"> Retonar Sucesso</response>
    /// <response code="401"> Não Autenticado</response> 
    /// <response code="404"> Usuário não encontrado</response>
    [Authorize]
    [HttpPatch("changePassword")]
    public IActionResult ChangePassword(string newPassword)
    {
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        var user = _userRepository.GetUserByEmail(userEmail);

        if (user == null)
        {
            return NotFound("Usuário não encontrado!");
        }
        else
        {
            user.ChangeUserPassword(newPassword, _passwordHasher);

            return Ok("Senha alterada com sucesso");
        }
    }

    /// <summary>
    /// Deleta usuário, o método necessita de permissão de Administrador.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"> Retonar Sucesso</response>
    /// <response code="401"> Não Autenticado</response>
    /// <response code="403"> Ñão Autorizado</response>
    /// <response code="404"> Usuário não encontrado</response>
    [Authorize]   
    [HttpDelete("deleteUser/{id}")]
    public IActionResult DeleteUser(int id)
    {        

        var user = _userRepository.GetById(id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado!");
        }           
        else
        {
            _userRepository.Delete(id);
            return Ok("Usuario deletado com sucesso");
        }       
    }
     




}
