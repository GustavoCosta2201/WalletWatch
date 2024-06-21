using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

builder.Services
    .AddIdentityApiEndpoints<PessoaComAcesso>()
    .AddEntityFrameworkStores<ConnectionDB>();

builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7089",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7015"])
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();
app.UseStaticFiles();

app.UseCors("wasm");

app.UseAuthorization();

app.MapControllers();

app.AddEndpointsUsuarios();
app.AddEndpointsTransacoes();
app.AddEndpointsCategorias();

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorização");
app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Autorização");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
