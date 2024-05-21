namespace WalletWatch.API.Requests
{
    public record class TransacoesRequest(int id_usuario, int id_categoria, string descricao, decimal valor, DateTime data);
  
}
