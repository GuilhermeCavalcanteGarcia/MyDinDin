using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Models;

namespace MyDinDin.Services;

public class UserService
{
    private readonly MyDinDinContext _contexto;

    public UserService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
    public User RegisterUser(User userNew)
    {
        // Adiocionando o novo curso no banco de dados
        _contexto.User.Add(userNew);

        // Salvando as alterações feitas no banco de dados
        _contexto.SaveChanges();

        // Retorno opcional apenas para confirmar que o novo user foi cadastrado
        return userNew;
    }

    // Selecionando todos os Users da tabela User do meu Bando de Dados
    public List<User> ToRecoverUsers()
    {
        return _contexto.User.ToList();
    }

    // Retornando um usuário específico a partir do Id passado na rota
    public User ToRecoverSpecificUser(int id)
    {
        User user = _contexto.User.SingleOrDefault(u => u.Id == id);
        if(user is null)
        {
            throw new Exception("Usuário não encontrado.");
        }
        return user;
    }
    public User UpdateUser(int id, User userUpdate)
    {
        User user = _contexto.User.SingleOrDefault(u => u.Id == id);

        // Verificando se o user é nulo a partir do retorno dado pelo SingleOrDefault armazenado em user
        if(user is null)
        {
            // Exceção para caso o user seja nulo
            throw new Exception("Usuário não encontrado");
        }

        // Recebendo no meu user do contexto os novos valores passados como parâmetro no userUpdate
        user.Nome = userUpdate.Nome;
        user.Email = userUpdate.Email;
        user.Senha = userUpdate.Senha;
        user.Password_Salt = userUpdate.Senha;
        
        // Salvando no Banco de dados as alterações feitas
        _contexto.SaveChanges();

        // Retorno dos dados atualizados do user
        return user;

        
    }
    public void DropUser(int id)
    {
        User user = _contexto.User.SingleOrDefault(u => u.Id == id);

        // Verificando se o user é nulo
        if(user is null)
        {
            // Lançando exceção caso o user seja nulo
            throw new Exception("Usuário não encontrado");
        }

        // Removendo usuário a partir do usuário armazenado na variável user da linha 68
        _contexto.Remove(user);

        // Salvando as alterações feitas no Banco de Dados
        _contexto.SaveChanges();
    }
}
