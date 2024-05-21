using Microsoft.VisualBasic;

namespace WalletWatch.API.Requests
{
    public record class TransacoesRequestEdit(int id_transacao, int id_usuario, int id_categoria, string descricao, decimal valor, DateTime data) 
        : TransacoesRequest(id_usuario, id_categoria, descricao, valor, data);

}
