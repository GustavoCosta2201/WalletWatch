namespace WalletWatch.Web.Response
{
    public record class TransacoesResponse (int id_transacao, int id_usuario, int id_categoria, string descricao, decimal valor, DateOnly data)
    {

        public override string ToString()
        {
            return $"{id_transacao} - {descricao} - {valor} - {data}";
        }
    }
}
