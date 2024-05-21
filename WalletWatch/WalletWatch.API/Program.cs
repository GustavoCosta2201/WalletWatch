using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using WalletWatch.API.Endpoints;
using WalletWatch.Banco;
using WalletWatch.Modelos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConnectionDB>();
builder.Services.AddTransient<DAL<Usuarios>>();
builder.Services.AddTransient<DAL<Transacoes>>();
builder.Services.AddTransient<DAL<Categorias>>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseStaticFiles();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.AddEndpointsUsuarios();
app.AddEndpointsTransacoes();
app.AddEndpointsCategorias();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
