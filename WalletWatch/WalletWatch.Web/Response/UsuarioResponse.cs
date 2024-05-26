namespace WalletWatch.Web.Response
{
    public record class UsuarioResponse(int Id_usuario, string nome, string senha, string email)
    {
        public override string ToString()
        {
            return $"{Id_usuario} - {nome} - {senha}";
        }
    }
}
