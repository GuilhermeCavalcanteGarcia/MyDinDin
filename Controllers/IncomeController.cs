using Microsoft.AspNetCore.Mvc;
using MyDinDin.Dtos;
using MyDinDin.Services;

namespace MyDinDin.Controllers;

[ApiController]
[Route("income")]
public class IncomeController : ControllerBase
{
    private readonly IncomeService _incomeService;
    public IncomeController([FromServices] IncomeService incomeService)
    {
        _incomeService = incomeService;
    }
    [HttpPost]
    public ActionResult<IncomeResponseDtos> PostIncome([FromBody] IncomeCreateAndUpdateDtos incomeNewDtos)
    {
        var incomeResponse = _incomeService.RegisterIncome(incomeNewDtos);

        return CreatedAtAction(nameof(GetIncome), new { id = incomeResponse.Id }, incomeResponse);  
    }

    [HttpGet]
    public ActionResult<List<IncomeResponseDtos>> GetIncome()
    {
        try
        {
            var income = _incomeService.ToRecoverIncome();

            return Ok(income);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut("{id:int}")]
    public ActionResult<IncomeResponseDtos> PutIncome([FromRoute] int id, [FromBody] IncomeCreateAndUpdateDtos incomeUpdate)
    {
        try
        {
            var incomeResponse = _incomeService.UpdateIncome(id, incomeUpdate);

            return Ok(incomeResponse);
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
            _incomeService.DropIncome(id);

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
