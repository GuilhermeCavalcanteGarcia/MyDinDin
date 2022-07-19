using Microsoft.EntityFrameworkCore;
using MyDinDin.Data;
using MyDinDin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adicionando a configuração para usar o Contexto no acesso ao BD
builder.Services.AddDbContext<MyDinDinContext>(
    //Informando que quero usar o MySql
    options => options.UseMySql(
        //Pegando o enderaço do servidor 
        builder.Configuration.GetConnectionString("ConectionDataBase"),
        //Detectando automaticamente a versao do servidor instalado 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConectionDataBase"))
    )

);

//Nossas classes de serviços criadas
                // Transient - Um novo objeto é criado onde pedir para ser adicionado.
                // Scoped - A maioria dos casos usa scoped, é criado o serviço durante uma única vez.
                // Singleton - Define que o objeto vai ser criado uma única vez, objeto vai ser igual para todas as requisições.
builder.Services.AddScoped<UserService>(); 
builder.Services.AddScoped<ExpenseService>(); 
builder.Services.AddScoped<GoalService>(); 
builder.Services.AddScoped<IncomeService>(); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

