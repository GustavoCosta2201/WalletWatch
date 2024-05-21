namespace WalletWatch.Web.Response
{
    public record class UsuarioResponse(int id, string nome, string senha, string email)
    {
        public override string ToString()
        {
            return $"{nome} - {senha}";
        }
    }
}
