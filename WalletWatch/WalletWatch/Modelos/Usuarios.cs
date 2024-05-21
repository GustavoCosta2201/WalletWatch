
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;
using System.IO;

namespace WalletWatch.Modelos
{

    public  class Usuarios
    {

        public Usuarios(string nome, string email, string senha)
        {

            Nome = nome;
            Senha = senha;
            Email = email;

        }

        public Usuarios() { }

        [Key]
        //[Column("ID_USUARIO")]
        public int Id_Usuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; } = "Default@gmail.com";
        public string? Senha { get; set; }


        public override string ToString()
        {

            return $"Usuário: {Nome} - Senha: {Senha} ";
        }

        public void ListarUsuarios()
        {
            var context = new ConnectionDB();
            var usuarios = new DAL<Usuarios>(context);

            var listaUsuarios = usuarios.Listar();

            foreach (var item in listaUsuarios)
            {
                Console.WriteLine($"Nome: {item.Nome}");
                Console.WriteLine($"Senha: {item.Senha}");
                Console.WriteLine($"Email: {item.Email}");

            }

        }

        public void AtualizarTabela(string email)
        {
            var context = new ConnectionDB();
            var usuarios = new Usuarios();
            var usuariosDAL = new DAL<Usuarios>(context);


            var EmailNullos = usuariosDAL.Listar().Where(e => e.Email == null).ToList();

            foreach (var Email in EmailNullos)
            {
                Email.Email = email;
            }
            context.SaveChanges();
        }

        public void ExportarClientetxt()
        {
            var context = new ConnectionDB();
            string path = "C:\\3- Estudos\\C#\\";
            string FullPath = path + "Cliente.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = File.Create(FullPath))
            {
                fs.Close(); 
            }
           
            if (File.Exists(FullPath))
            {
                using (StreamWriter sw = new StreamWriter(FullPath))
                {
                    var usuarioDal = new DAL<Usuarios>(context);
                    var ListaUsuarios = usuarioDal.Listar();

                    foreach (var item in ListaUsuarios)
                    {
                        sw.WriteLine($"Nome do Usuario: - {item.Nome}");
                    }
                    Console.WriteLine("Lista de Usuários exportado com Sucesso!");
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado");
            }
        }
    }
}
