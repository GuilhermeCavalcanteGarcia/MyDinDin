using Microsoft.AspNetCore.Mvc;
using MyDinDin.Dtos;
using MyDinDin.Models;
using MyDinDin.Services;

namespace MyDinDin.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController([FromServices] UserService userService)
    {
        _userService = userService;
 
    }
    [HttpPost]
    public ActionResult<UserResponseDtos> PostUser([FromBody] UserCreateAndUpdateDtos userNewDtos)
    {
        var userResponse = _userService.RegisterUser(userNewDtos);

        // TIRAR DÚVIDA SOBRE A FUNCIONALIDADE DA LINHA ABAIXO, EXPLICAR O QUE A LINHA ABAIXO FAZ
        return CreatedAtAction(nameof(GetUsers), new { id = userResponse.Id }, userResponse);  
    }

    // Verbo que vai retornar o user seguindo as propiedades da classe Dtos.UserResponseDtos
    [HttpGet]
    public ActionResult<List<UserResponseDtos>> GetUsers()
    {
        try
        {
            var users = _userService.ToRecoverUsers();

            return Ok(users);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<UserResponseDtos> GetUserId([FromRoute] int id)
    {
        try
        {
            var userResponse = _userService.ToRecoverSpecificUser(id);

            return userResponse;
        }
        catch(Exception)
        {
            return NotFound();

        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<UserResponseDtos> PutUser([FromRoute] int id, [FromBody] UserCreateAndUpdateDtos userUpdate)
    {
        try
        {
            var userResponse = _userService.UpdateUser(id, userUpdate);

            return Ok(userResponse);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }

    // PORQUE NÃO PRECISA ESPECIFICAR NA ROTA O ID ? ID ESPECIFICADO PELO HTTP
    [HttpDelete("{id:int}")]
    public ActionResult Delete([FromRoute] int id)
    {
        try
        {
            // Chamando o método do UserService que exclui da tabela usuário o usuário especificado na rota 
            _userService.DropUser(id);

            // Retorna 204 confirmando que foi deletado
            return NoContent();
        }
        catch(Exception)
        {
            // Retorna 404 avisando de um erro
            return NotFound();
        }
    }
}
