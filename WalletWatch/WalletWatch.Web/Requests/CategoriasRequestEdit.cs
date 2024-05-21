namespace WalletWatch.API.Requests
{
    public record class CategoriasRequestEdit(int id,string nome, string tipo) : CategoriasRequest(nome, tipo);
 
}
