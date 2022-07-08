using hjs_encomiendas_servidor.Dominio;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddScoped<IDominio, dUsuario>();
builder.Services.AddDbContext<UsuarioContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("hjsConnection")));

builder.Services.AddCors(c =>
 {
     c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
      .AllowAnyHeader());
 });


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod()
            .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
