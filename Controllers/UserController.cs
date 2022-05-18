using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<User> PostUser([FromBody] User userNew)
    {
        User user = _userService.RegisterUser(userNew);

        // TIRAR DÚVIDA SOBRE A FUNCIONALIDADE DA LINHA ABAIXO, EXPLICAR O QUE A LINHA ABAIXO FAZ
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
    [HttpGet]
    public ActionResult<List<User>> GetUsers()
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
    public ActionResult<User> GetUserId([FromRoute] int id)
    {
        try
        {
            User user = _userService.ToRecoverSpecificUser(id);

            return user;
        }
        catch(Exception)
        {
            return NotFound();

        }
    }
    [HttpPut("{id:int}")]
    public ActionResult<User> PutUser([FromRoute] int id, [FromBody] User userUpdate)
    {
        try
        {
            User user = _userService.UpdateUser(id, userUpdate);

            return Ok(user);
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
