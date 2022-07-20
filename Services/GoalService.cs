using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Dtos;
using MyDinDin.Models;

namespace MyDinDin.Services;

public class GoalService
{
    private readonly MyDinDinContext _contexto;

    public GoalService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
    public GoalResponseDtos RegisterGoal(GoalCreateAndUpdateDtos goalNew)
    {
        var goal = goalNew.Adapt<Goal>();
        _contexto.Goal.Add(goal);

        _contexto.SaveChanges();

        var goalResponse = goal.Adapt<GoalResponseDtos>();
        return goalResponse;
    }
    public List<GoalResponseDtos> ToRecoverGoal()
    {
        var goal = _contexto.Goal.ToList();

        var goalResponse = goal.Adapt<List<GoalResponseDtos>>();

        return goalResponse;

    }
    public GoalResponseDtos ToRecoverSpecificGoal(int id)
    {
        var goal = _contexto.Goal.SingleOrDefault(g => g.Id == id);
        if(goal is null)
        {
            throw new Exception("Meta não encontrada.");
        }

        var goalResponse = goal.Adapt<GoalResponseDtos>();
        return goalResponse;
    }
    public GoalResponseDtos UpdateGoal(int id, GoalCreateAndUpdateDtos goalUpdate)
    {
        Goal goal = _contexto.Goal.SingleOrDefault(g => g.Id == id);

        if(goal is null)
        {
            throw new Exception("Meta não encontrada");

        }
        goalUpdate.Adapt(goal);
        
        // Salvando no Banco de dados as alterações feitas
        _contexto.SaveChanges();
        
        var goalResponse = goalUpdate.Adapt<GoalResponseDtos>();
      
        // Retorno dos dados atualizados do goal       
        return goalResponse;

        
    }
    public void DropGoal(int id)
    {
        Goal goal = _contexto.Goal.SingleOrDefault(g => g.Id == id);

        // Verificando se o user é nulo
        if(goal is null)
        {
            // Lançando exceção caso o user seja nulo
            throw new Exception("Goal não encontrada");
        }

        // Removendo meta a partir da meta armazenada na variável user da linha 59
        _contexto.Remove(goal);

        // Salvando as alterações feitas no Banco de Dados
        _contexto.SaveChanges();
    }

}
