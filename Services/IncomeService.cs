using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Dtos;
using MyDinDin.Models;

namespace MyDinDin.Services;

public class IncomeService
{
    private readonly MyDinDinContext _contexto;

    public IncomeService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
    public IncomeResponseDtos RegisterIncome(IncomeCreateAndUpdateDtos incomeNew)
    {
        var income = incomeNew.Adapt<Income>();
        _contexto.Income.Add(income);

        _contexto.SaveChanges();

        var incomeResponse = income.Adapt<IncomeResponseDtos>();
        return incomeResponse;
    }
    public List<IncomeResponseDtos> ToRecoverIncome()
    {
        var income = _contexto.Income.ToList();

        var incomeResponse = income.Adapt<List<IncomeResponseDtos>>();

        return incomeResponse;

    }
    public IncomeResponseDtos UpdateIncome(int id, IncomeCreateAndUpdateDtos incomeUpdate)
    {
        Income income = _contexto.Income.SingleOrDefault(i => i.Id == id);

        if(income is null)
        {
            throw new Exception("Receita não encontrada");

        }
        incomeUpdate.Adapt(income);
        
        // Salvando no Banco de dados as alterações feitas
        _contexto.SaveChanges();
        
        var incomeResponse = incomeUpdate.Adapt<IncomeResponseDtos>();
      
        // Retorno dos dados atualizados do income       
        return incomeResponse;

        
    }
    public void DropIncome(int id)
    {
        Income income = _contexto.Income.SingleOrDefault(i => i.Id == id);

        // Verificando se o income é nulo
        if(income is null)
        {
            // Lançando exceção caso o user seja nulo
            throw new Exception("Receita não encontrada");
        }

        // Removendo income a partir da income armazenada na variável income da linha 59
        _contexto.Remove(income);

        // Salvando as alterações feitas no Banco de Dados
        _contexto.SaveChanges();
    }
}
