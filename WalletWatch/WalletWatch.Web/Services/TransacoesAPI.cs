using System.Net.Http;
using System.Net.Http.Json;
using WalletWatch.API.Requests;
using WalletWatch.Web.Response;

namespace WalletWatch.Web.Services
{
    public class TransacoesAPI
    {

        private readonly HttpClient httpClient;

        public TransacoesAPI(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<TransacoesResponse>?> GetTransacoesAsync()
        {
            return await httpClient.GetFromJsonAsync<ICollection<TransacoesResponse>>("Transacoes");
        }

        public async Task<TransacoesResponse?> GetTransacoesPorNomeAsync(string nome)
        {
            return await httpClient.GetFromJsonAsync<TransacoesResponse>($"/Transacoes/{nome}");
        }

        public async Task<TransacoesResponse?> GetTransacaoPorIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<TransacoesResponse>($"transacoes/{id}");
        }

        public async Task AddTransacoesAsync(TransacoesRequest transrquest)
        {
            await httpClient.PostAsJsonAsync("Transacoes", transrquest);
        }

        public async Task<bool> DeleteTransacoesAsync(int id)
        {
            await httpClient.DeleteAsync($"/Transacoes/{id}");
            return true;
        }

        public async Task<bool> UpdateTransacoesAsync(TransacoesRequestEdit transedit)
        {

            var url = $"/Transacoes/{transedit.Id_Transacao}";
            await httpClient.PutAsJsonAsync(url, transedit);
            return true;
        }


    }
}
