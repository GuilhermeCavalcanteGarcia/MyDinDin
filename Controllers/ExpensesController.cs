using Microsoft.AspNetCore.Mvc;
using MyDinDin.Dtos;
using MyDinDin.Services;

namespace MyDinDin.Controllers;

[ApiController]
[Route("expenses")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseService _expenseService;
    public ExpensesController([FromServices] ExpenseService expenseService)
    {
        _expenseService = expenseService;
    }
    [HttpPost]
    public ActionResult<ExpensesResponseDtos> PostExpense([FromBody] ExpensesCreateAndUpdateDto expenseNewDtos)
    {
        var expenseResponse = _expenseService.RegisterExpense(expenseNewDtos);

        // TIRAR DÚVIDA SOBRE A FUNCIONALIDADE DA LINHA ABAIXO, EXPLICAR O QUE A LINHA ABAIXO FAZ
        return CreatedAtAction(nameof(GetExpenses), new { id = expenseResponse.Id }, expenseResponse);  
    }
    [HttpGet]
    public ActionResult<List<ExpensesResponseDtos>> GetExpenses()
    {
        try
        {
            var expenses = _expenseService.ToRecoverExpenses();

            return Ok(expenses);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<ExpensesResponseDtos> GetExpenseId([FromRoute] int id)
    {
        try
        {
            var expenseResponse = _expenseService.ToRecoverSpecificExpense(id);

            return expenseResponse;
        }
        catch(Exception)
        {
            return NotFound();

        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<ExpensesResponseDtos> PutExpense([FromRoute] int id, [FromBody] ExpensesCreateAndUpdateDto expenseUpdate)
    {
        try
        {
            var expenseResponse = _expenseService.UpdateExpense(id, expenseUpdate);

            return Ok(expenseResponse);
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
            // Chamando o método ExpenseService que exclui da tabela Expenses o Expense especificado na rota 
            _expenseService.DropExpense(id);

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
