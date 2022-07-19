using Microsoft.AspNetCore.Mvc;
using MyDinDin.Dtos;
using MyDinDin.Services;

namespace MyDinDin.Controllers;


[ApiController]
[Route("goal")]
public class GoalController : ControllerBase
{
    private readonly GoalService _goalService;
    public GoalController([FromServices] GoalService goalService)
    {
        _goalService = goalService;
    }
    [HttpPost]
    public ActionResult<GoalResponseDtos> PostGoal([FromBody] GoalCreateAndUpdateDtos goalNewDtos)
    {
        var goalResponse = _goalService.RegisterGoal(goalNewDtos);

        return CreatedAtAction(nameof(GetGoal), new { id = goalResponse.Id }, goalResponse);  
    }
    [HttpGet]
    public ActionResult<List<GoalResponseDtos>> GetGoal()
    {
        try
        {
            var goal = _goalService.ToRecoverGoal();

            return Ok(goal);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut("{id:int}")]
    public ActionResult<GoalResponseDtos> PutGoal([FromRoute] int id, [FromBody] GoalCreateAndUpdateDtos goalUpdate)
    {
        try
        {
            var goalResponse = _goalService.UpdateGoal(id, goalUpdate);

            return Ok(goalResponse);
        } 
        catch(Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete([FromRoute] int id)
    {
        try
        {
            // Chamando o método GoalService que exclui da tabela Goal o Goal especificado na rota 
            _goalService.DropGoal(id);

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
