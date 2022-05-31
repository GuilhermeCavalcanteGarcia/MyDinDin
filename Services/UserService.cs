using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Dtos;
using MyDinDin.Models;

namespace MyDinDin.Services;

public class UserService
{
    private readonly MyDinDinContext _contexto;

    public UserService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
    public UserResponseDtos RegisterUser(UserCreateAndUpdateDtos userNewDto)
    {
        var user = userNewDto.Adapt<User>();

        // Regra de negócia que vai criptografar a senha
        user.Password_Salt = userNewDto.Senha + "TESTE_DE_CRIPTOGRAFIA";
        // Adiocionando o novo curso no banco de dados
        _contexto.User.Add(user);

        // Salvando as alterações feitas no banco de dados
        _contexto.SaveChanges();

        var userResponse = user.Adapt<UserResponseDtos>();
        // Retorno opcional apenas para confirmar que o novo user foi cadastrado
        return userResponse;
    }

    // Selecionando todos os Users da tabela User do meu Bando de Dados
    public List<UserResponseDtos> ToRecoverUsers()
    {
        // Recebendo user que retorna os campos padrões, campos esses quesão os mesmos da classe Models.User
        var user = _contexto.User.ToList();

        // Passando de forma automatizada os users para o forma do Dto(Data Transfer Object), referência da tabela Dtos.UserResponseDto
        // Dessa forma não retornamos dados que devem ser tratados apenas no banco de dados, como por exemplo o ID e as Senhas.
        var userResponse = user.Adapt<List<UserResponseDtos>>();

        // Retornando variável que recebeu a transformação automática do modelo Models.User para Dtos.UserResponseDtos
        return userResponse;

    }

    // Retornando um usuário específico a partir do Id passado na rota
    public UserResponseDtos ToRecoverSpecificUser(int id)
    {
        var user = _contexto.User.SingleOrDefault(u => u.Id == id);
        if(user is null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        var userResponse = user.Adapt<UserResponseDtos>();
        return userResponse;
    }
    public UserResponseDtos UpdateUser(int id, UserCreateAndUpdateDtos userUpdate)
    {
        User user = _contexto.User.SingleOrDefault(u => u.Id == id);

        // Verificando se o user é nulo a partir do retorno dado pelo SingleOrDefault armazenado em user
        if(user is null)
        {
            // Exceção para caso o user seja nulo
            throw new Exception("Usuário não encontrado");

        }
        // Criptografia temporária, onde o campo Password_Salt recebe a senha do userUpdate concatenada com a criptografia
        user.Password_Salt = userUpdate.Senha + "Cript";

        // Passando o user do tipo Dtos.UserCreateAndUpdate para Models.User
        userUpdate.Adapt(user);

        /*  Fazendo a adaptação do userUpdate que era do tipo Dtos.UserCreateAndUpdate, 
            passou a ser Models.User e agora será transformado em Dtos.UserResponseDtos.
        */
        var userResponse = userUpdate.Adapt<UserResponseDtos>();

        // Salvando no Banco de dados as alterações feitas
        _contexto.SaveChanges();
        
        // Retorno dos dados atualizados do user        
        return userResponse;

        
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
