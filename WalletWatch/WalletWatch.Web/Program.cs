using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WalletWatch.Web;
using MudBlazor.Services;
using WalletWatch.Web.Services;
using System.Globalization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var culture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

builder.Services.AddMudServices();

builder.Services.AddTransient<UsuariosAPI>();
builder.Services.AddTransient<CategoriaAPI>();
builder.Services.AddTransient<TransacoesAPI>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");

});

await builder.Build().RunAsync();

