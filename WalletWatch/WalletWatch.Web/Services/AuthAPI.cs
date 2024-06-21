using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using WalletWatch.Web.Response;

namespace WalletWatch.Web.Services
{
    public class AuthAPI(IHttpClientFactory factory) : AuthenticationStateProvider
    {

        private readonly HttpClient httpClient = factory.CreateClient("API");
        private bool autenticado = false;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            autenticado = false;
            var pessoa = new ClaimsPrincipal();
            var response = await httpClient.GetAsync("auth/manage/info");

            if (response.IsSuccessStatusCode)
            {
                var info = await response.Content.ReadFromJsonAsync<InfoPessoaResponse>();
                Claim[] dados =
                [
                    new Claim(ClaimTypes.Name, info.Email),
                    new Claim(ClaimTypes.Email, info.Email)
                ];

                var identity = new ClaimsIdentity(dados, "Cookies");
                pessoa = new ClaimsPrincipal(identity);
                autenticado = true;
            }

            return new AuthenticationState(pessoa);
        }

        public async Task<AuthResponse> Cadastrar(string? email, string? senha)
        {
            var response = await httpClient.PostAsJsonAsync("auth/register", new
            {
                Email    = email,
                password = senha
            });

            if (response.IsSuccessStatusCode) {

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                await LoginAsync(email, senha);
                return new AuthResponse { Sucesso = true };
            }
            else
            {
                return new AuthResponse { Sucesso = false, Erros = ["Senha não tem os requisitos necessários!"] };
            }
        }
        public async Task<AuthResponse> LoginAsync(string? email, string? senha)
        {
            var response = await httpClient.PostAsJsonAsync("auth/login?useCookies=true", new
            {
                email,
                password = senha
            });

            if (response.IsSuccessStatusCode)
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new AuthResponse { Sucesso = true };
            }
            else
            {
                return new AuthResponse
                {
                    Sucesso = false,
                    Erros = ["Login/Senha inválida"]
                };
            }
        }

        public async Task LogoutAsync()
        {
            await httpClient.PostAsync("auth/logout", null);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<bool> VerificaAutenticado()
        {
            await GetAuthenticationStateAsync();
            return autenticado;
        }

    }
}
