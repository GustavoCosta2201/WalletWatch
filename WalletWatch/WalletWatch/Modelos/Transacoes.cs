using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;
using WalletWatch.Menu;

namespace WalletWatch.Modelos
{
    public class Transacoes : Titulo
    {

        /*public Transacoes(string descricao, decimal valor, DateTime data_transacao)
        {

            Descricao = descricao;
            Valor = valor;
            Data_Transacao = data_transacao;
        }*/
        [Key]
        public int Id_Transacao { get; set; }
        public int Id_Usuario { get; set; }
/*        public virtual Usuarios Usuario { get; set; }*/
        public int Id_Categoria { get; set; }
  /*      public virtual Categorias Categoria { get; set; }*/
        public string? Descricao { get; set; } = "Descrição Padrão";
        public decimal Valor { get; set; }
        public DateTime Data_Transacao { get; set; } = DateTime.Now;

        public Transacoes()
        {

        }
        public Transacoes(int id_usuario, int id_categoria, string descricao, decimal valor, DateTime data)
        {
            this.Id_Usuario = id_usuario;
            this.Id_Categoria = id_categoria;
            this.Descricao = descricao;
            this.Valor = valor;
            this.Data_Transacao = data;
        }



        public override string ToString()
        {
            return $"Descrição: {Descricao} - Valor: {Valor}";
        }

        public void ListarTransacoes()
        {
            var context = new ConnectionDB();
            var transaction = new DAL<Transacoes>(context);
            var listaTransacoes = transaction.Listar();
            int i = 0;
            foreach (var item in listaTransacoes)
            {
                i++;
                Console.WriteLine($"{i} - {item.Descricao}- {item.Valor} - {item.Data_Transacao}");
            }

        }

        public void ExportarTransacoestxt()
        {
            var context = new ConnectionDB();
            var TransactionDAL = new DAL<Transacoes>(context);
            var UsuarioDAL = new DAL<Usuarios>(context);
            var CategoriasDAL = new DAL<Categorias>(context);

            string path = "C:\\3- Estudos\\C#\\";
            string FullPath = path + "Transacoes.txt";

            try
            {

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (!File.Exists(FullPath))
                {
                    using (FileStream fs = File.Create(FullPath))
                    {
                        fs.Close();
                    }
                }

                using (StreamWriter sw = new StreamWriter(FullPath))
                {
                    decimal total = 0;
                    var listaTransacoes = TransactionDAL.Listar();
                    sw.Write("Id da Transação\t   - Usuário\t     -     Categoria\t     -     Descrição\t                  -    Valor\t           -            Data\n".ToUpper());
                    foreach (var item in listaTransacoes)
                    {
                        var recuperarUsuario = UsuarioDAL.RecuperarPor(v => v.Id_Usuario.Equals(item.Id_Usuario));
                        var recuperarCategoria = CategoriasDAL.RecuperarPor(v => v.Id_Categoria.Equals(item.Id_Categoria));

                        if (recuperarUsuario != null && recuperarCategoria != null)
                        {
                            var transaction = new Transacoes();
                            
                            sw.WriteLine($"\n{item.Id_Transacao}\t  -  Usuário: {recuperarUsuario.Nome}\t  -  {recuperarCategoria.Nome}\t  -  {item.Descricao}\t - {item.Valor}\t - {item.Data_Transacao}\t");
                            total += item.Valor;
                        }
                        else
                        {
                            Console.WriteLine("Usuário ou Categoria não encontrados.");
                        }
                    }

                    sw.WriteLine($"\nTotal: {total}");
                    Console.WriteLine("Transações exportadas com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
