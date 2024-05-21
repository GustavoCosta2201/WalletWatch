using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;

namespace WalletWatch.Modelos
{
    public class Categorias
    {
        public virtual ICollection<Transacoes>? Transacoes { get; set; }
        public Categorias(string Nome, string Tipo)
        {

            this.Nome = Nome;
            this.Tipo = Tipo;
        }

        public Categorias() { }
        [Key]

        public int Id_Categoria { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }



        public override string ToString()
        {
            return $"Nome da Categoria: {Nome} - Tipo: {Tipo}";
        }

        public void ListarCategorias()
        {
            var context = new ConnectionDB();
            var categorias = new DAL<Categorias>(context);

            var ListaCategorias = categorias.Listar();

            foreach (var c in ListaCategorias)
            {
                Console.WriteLine($"Nome: {c.Nome } - Tipo: {c.Tipo}");
            }

        }


        public void ExportarCategoriastxt()
        {
            var context = new ConnectionDB();

            string path = "C:\\3- Estudos\\C#\\";
            string arquivo = path + "Categoria.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {

                if (!File.Exists(arquivo))
                {
                    using (FileStream fs = File.Create(arquivo))
                    {
                        fs.Close();
                    }
                }

                var categoriadal = new DAL<Categorias>(context);
                var ListaCategorias = categoriadal.Listar();

                using (StreamWriter sw = new StreamWriter(arquivo))
                {
                    int n = 0;
                    foreach (var c in ListaCategorias)
                    {
                        n++;
                        sw.WriteLine($" {n} - {c.Nome} - {c.Tipo} \n");
                    }

                }
                Console.WriteLine("Categoria exportada com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
