using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Dtos;
using MyDinDin.Models;

namespace MyDinDin.Services;

public class ExpenseService
{
    private readonly MyDinDinContext _contexto;

    public ExpenseService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
    public ExpensesResponseDtos RegisterExpense(ExpensesCreateAndUpdateDto expenseNew)
    {
        var expense = expenseNew.Adapt<Expenses>();
        _contexto.Expenses.Add(expense);

        _contexto.SaveChanges();

        var expenseResponse = expense.Adapt<ExpensesResponseDtos>();
        return expenseResponse;
    }
    
    public List<ExpensesResponseDtos> ToRecoverExpenses()
    {
        var expense = _contexto.Expenses.ToList();

        var expenseResponse = expense.Adapt<List<ExpensesResponseDtos>>();

        return expenseResponse;

    }
    public ExpensesResponseDtos ToRecoverSpecificExpense(int id)
    {
        var expense = _contexto.Expenses.SingleOrDefault(u => u.Id == id);
        if(expense is null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        var expenseResponse = expense.Adapt<ExpensesResponseDtos>();
        return expenseResponse;
    }
    public ExpensesResponseDtos UpdateExpense(int id, ExpensesCreateAndUpdateDto expenseUpdate)
    {
        Expenses expense = _contexto.Expenses.SingleOrDefault(e => e.Id == id);

        if(expense is null)
        {
            throw new Exception("Despesa não encontrada");

        }
        expenseUpdate.Adapt(expense);
        
        // Salvando no Banco de dados as alterações feitas
        _contexto.SaveChanges();
        
        var expenseResponse = expenseUpdate.Adapt<ExpensesResponseDtos>();
      
        // Retorno dos dados atualizados do user        
        return expenseResponse;

        
    }
    public void DropExpense(int id)
    {
        Expenses expense = _contexto.Expenses.SingleOrDefault(e => e.Id == id);

        // Verificando se o user é nulo
        if(expense is null)
        {
            // Lançando exceção caso o user seja nulo
            throw new Exception("Despesa não encontrada");
        }

        // Removendo usuário a partir do usuário armazenado na variável user da linha 68
        _contexto.Remove(expense);

        // Salvando as alterações feitas no Banco de Dados
        _contexto.SaveChanges();
    }

}
