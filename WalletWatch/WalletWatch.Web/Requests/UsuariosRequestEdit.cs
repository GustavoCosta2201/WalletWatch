namespace WalletWatch.API.Requests
{
    public record class UsuariosRequestEdit(int id, string nome, string senha, string email) : UsuariosRequest(nome, senha, email);

}
