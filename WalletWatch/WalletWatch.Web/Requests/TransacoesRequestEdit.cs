
using Microsoft.VisualBasic;

namespace WalletWatch.API.Requests
{
    public class TransacoesRequestEdit
    {
        public int Id_Transacao { get; init; }
        public int Id_Usuario { get; set; }
        public int Id_Categoria { get; set; }
        public string descricao { get; set; }
        public decimal Valor { get; set; }
        public DateOnly? Data { get; set; }

        public TransacoesRequestEdit(int id_transacao, int id_usuario, int id_categoria, string descricao, decimal valor, DateOnly? data)
        {
            this.Id_Transacao = id_transacao;
            this.Id_Usuario = id_usuario;
            this.descricao = descricao;
            this.Valor = valor;
            this.Id_Categoria = id_categoria;
            this.Data = data;
        }
    }



}
