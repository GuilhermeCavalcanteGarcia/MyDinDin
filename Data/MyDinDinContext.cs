using Microsoft.EntityFrameworkCore;
using MyDinDin.Models;

namespace MyDinDin.Data;

// Contrutor que vai receber as configurações
// Para Usar o contexto do EF core(Entity FrameWork Core) 
// Cofigurações serão passadas no main
// Arquivo Program.cs

public class MyDinDinContext : DbContext
{
    public MyDinDinContext(DbContextOptions<MyDinDinContext> options) : base(options)
    {
        
    }

    // Tabela que vai armazernar os cursos
    // DbSet representa a nossa tabela na memória
    // ou seja, representa nossa tabela no banco de dados.
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Expenses> Expenses { get; set; }
    public DbSet<Goal> Goal { get; set; }
    public DbSet<Income> Income { get; set; }
    public DbSet<User> User { get; set; }
}
