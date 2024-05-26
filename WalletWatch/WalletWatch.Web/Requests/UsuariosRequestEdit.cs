namespace WalletWatch.API.Requests
{

    public class UsuariosRequestEdit
    {

        public int Id_Usuario { get; init; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        
        public UsuariosRequestEdit(int Id_Usuario, string nome, string senha, string email)
        {
            this.Id_Usuario = Id_Usuario;
            this.nome = nome;   
            this.senha = senha;
            this.email = email;
        }
    }
    

}
