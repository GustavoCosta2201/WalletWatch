using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WalletWatch.API.Requests;
using WalletWatch.Web.Response;
using System;
using System.Collections.Generic;

namespace WalletWatch.Web.Services
{
    public class UsuariosAPI
    {
        private readonly HttpClient _httpClient;

        public UsuariosAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<UsuarioResponse>?> GetUsuarioAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ICollection<UsuarioResponse>>("usuarios");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao recuperar usuários: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
        }

        public async Task AddUsuarioAsync(UsuariosRequest usuario)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("usuarios", usuario);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"usuarios/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao deletar usuário: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<UsuarioResponse?> GetUsuarioPorNomeAsync(string nome)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UsuarioResponse>($"usuarios/{nome}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao recuperar usuário por nome: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateUsuarioAsync(UsuariosRequestEdit usuario)
        {
            try
            {
                await _httpClient.PutAsJsonAsync("usuarios", usuario);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
