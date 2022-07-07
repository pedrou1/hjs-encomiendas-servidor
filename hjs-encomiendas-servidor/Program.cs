using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddScoped<IDominio, UsuarioDom>();
builder.Services.AddDbContext<UsuarioContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("hjsConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
