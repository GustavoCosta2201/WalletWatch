namespace WalletWatch.API.Requests
{
    public class CategoriasRequestEdit
    {
        public int Id_Categoria { get; init; }
        public string nome { get; set; }
        public string tipo { get; set; }

        public CategoriasRequestEdit(int id_Categoria, string nome, string tipo)
        {
            Id_Categoria = id_Categoria;
            this.nome = nome;
            this.tipo = tipo;
        }
    }
}
