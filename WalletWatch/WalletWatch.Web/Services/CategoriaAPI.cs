using System.Collections;
using System.Net.Http.Json;
using WalletWatch.API.Requests;
using WalletWatch.Web.Response;

namespace WalletWatch.Web.Services
{
    public class CategoriaAPI
    {

        private readonly HttpClient httpClient;

        public CategoriaAPI(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient("API");
        }


        public async Task<ICollection<CategoriaResponse>?> GetCategoriaAsync()
        {
            return await httpClient.GetFromJsonAsync<ICollection<CategoriaResponse>>("Categorias");
        }

        public async Task<CategoriaResponse?> GetCategoriaPorIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<CategoriaResponse>($"/Categorias/{id}");
        }

        public async Task<CategoriaResponse?> GetCategoriaPorNome(string nome)
        {
            return await httpClient.GetFromJsonAsync<CategoriaResponse?>($"/Categorias/{nome}");
        }

        public async Task AddCategoriaAsync(CategoriasRequest catrequest)
        {
            await httpClient.PostAsJsonAsync("Categorias", catrequest);
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            await httpClient.DeleteAsync($"/Categorias/{id}");
            return true;
        }

        public async Task<bool> UpdateCategoriaAsync(CategoriasRequestEdit model)
        {
            var url = $"/Categorias/{model.Id_Categoria}";
            await httpClient.PutAsJsonAsync(url, model);

            return true;
        }


    }
}
