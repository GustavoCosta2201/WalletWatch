using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.Menu
{
    internal class GerenciarCategorias
    {
        public static void GerenciarCategoria()
        {
            var context = new ConnectionDB();
            var categoriaDAL = new DAL<Categorias>(context);
            Console.Clear();

            Titulo.TituloMenu("Gerenciador de Categorias");

            Dictionary<int, string> opcaoCategorias = new Dictionary<int, string>
                {
                    {1, "Cadastrar Categoria" },
                    {2, "Atualizar Categoria" },
                    {3, "Remover Categoria" },
                    {4, "Listar Categorias" },
                    {5, "Exportar Categoria" },
                    {6, "Sair" }
                };

            foreach (var op in opcaoCategorias)
            {
                Console.WriteLine($"{op.Key} - {op.Value}");
            }
            int opcao = LeituraEntrada.LerOpcao();

            switch (opcao)
            {
                case 1:
                    var categoria = new Categorias();
                    Console.WriteLine("Digite o nome da Categoria: ");
                    categoria.Nome = Console.ReadLine();
                    Console.WriteLine("Digite o tipo da Categoria Receita/Despesa");
                    categoria.Tipo = Console.ReadLine();
                    categoriaDAL.Adicionar(categoria);
                    Console.WriteLine("Categoria cadastrada com sucesso!");

                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 2:
                    var categorias = new Categorias();
                    Console.WriteLine("Digite a Categoria para atualizar:");
                    string atualizar = Console.ReadLine()!;
                    var categoriaAtualizada = categoriaDAL.RecuperarPor(c => c.Nome!.Equals(atualizar));

                    if (categoriaAtualizada != null)
                    {
                        Console.WriteLine("Digite a nova categoria: ");
                        categoriaAtualizada.Nome = Console.ReadLine();
                        Console.WriteLine("Digite o tipo da categoria");
                        categoriaAtualizada.Tipo = Console.ReadLine();
                        categoriaDAL.Atualizar(categoriaAtualizada);
                        Console.WriteLine("Categoria atualizada com sucesso!");

                        Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.WriteLine("Usuário não encontrado!");
                    }
                    break;


                case 3:
                    Console.WriteLine("Digite a categoria para excluir");
                    string exclusao = Console.ReadLine()!;
                    var categoriaExcluida = categoriaDAL.RecuperarPor(e => e.Nome!.Equals(exclusao));

                    if (categoriaExcluida != null)
                    {
                        categoriaDAL.Deletar(categoriaExcluida);
                        Console.WriteLine("Categoria excluída com sucesso!");

                        Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Categoria não encontrada!");
                    }
                    break;
                case 4:
                    var listaCategorias = new Categorias();
                    listaCategorias!.ListarCategorias();

                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 5:
                    var exportar = new Categorias();
                    exportar.ExportarCategoriastxt();
                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 6:
                    return;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }


        }

    }
}
