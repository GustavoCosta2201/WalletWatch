using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WalletWatch.API.Requests;
using WalletWatch.Web.Response;
using System;
using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.Reflection;

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

        public async Task<UsuarioResponse?> GetUsuarioPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioResponse>($"usuarios/{id}");
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

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"usuarios/{id}");
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao deletar usuário: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUsuarioAsync(UsuariosRequestEdit usuario)
        {
            try
            {
                var url = $"/Usuarios/{usuario.Id_Usuario}";
                await _httpClient.PutAsJsonAsync(url, usuario);

                return true; 
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
        }
    }
}
