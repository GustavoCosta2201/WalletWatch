namespace WalletWatch.Web.Response
{
    public record class CategoriaResponse(int Id_Categoria, string nome, string tipo)
    {

        public override string ToString()
        {
            return $"{Id_Categoria} - {nome} - {tipo}";
        }
    }
}
