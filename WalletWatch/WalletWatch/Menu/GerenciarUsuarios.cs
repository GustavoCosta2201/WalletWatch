using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.Menu
{
    internal class GerenciarUsuarios
    {
        public static void GerenciarUsuario()

        {
            var context = new ConnectionDB();
            var usuarioDAL = new DAL<Usuarios>(context);
            Console.Clear();

            Titulo.TituloMenu("Gerenciador de Usuários");

            Dictionary<int, string> opcaoUsuario = new Dictionary<int, string>()
             {
                {1, "Listar Usuários" },
                {2, "Cadastrar Usuário" },
                {3, "Atualizar Usuário" },
                {4, "Remover Usuário" },
                {5, "Exportar Usuários" },
                {6, "Sair" }
            };

            foreach (var item in opcaoUsuario)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            int opcao = LeituraEntrada.LerOpcao();

            if (opcaoUsuario.ContainsKey(opcao))
            {

                switch (opcao)
                {
                    case 1:
                        var ListaUsuarios = usuarioDAL.Listar();
                        int num = 0;
                        foreach (var item in ListaUsuarios)
                        {
                            num++;
                            Console.WriteLine($"{num} - Nome do Usuário: {item.Nome}\n");
                        }
                        Console.WriteLine("Digite uma tecla para voltar para o Menu Principal");
                        Console.ReadKey();
                        break;

                    case 2:
                        try
                        {
                            var usuario = new Usuarios();
                            Console.WriteLine("Digite o Nome do Usuário");
                            usuario.Nome = Console.ReadLine();
                            Console.WriteLine("Digite a Senha do Usuário");
                            usuario.Senha = Console.ReadLine();

                            usuarioDAL.Adicionar(usuario);
                            Console.WriteLine("Usuário Cadastrado com Sucesso");

                            Console.WriteLine("Digite uma tecla para voltar para o Menu Principal");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            Console.WriteLine("Digite o Nome do Usuário a ser atualizado");
                            string NomeUsuario = Console.ReadLine()!;
                            var UsuarioRecuperado = usuarioDAL.RecuperarPor(u => u.Nome!.Equals(NomeUsuario));

                            if (UsuarioRecuperado != null)
                            {
                                UsuarioRecuperado!.Nome = "testeAAtualizado";
                                usuarioDAL.Atualizar(UsuarioRecuperado!);
                                Console.WriteLine("Usuário Atualizado com Sucesso!");

                                Console.WriteLine("Digite uma tecla para voltar para o Menu Principal");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Usuário não encontrado");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Houve um erro inesperado: {ex.Message}");
                        }
                        break;

                    case 4:
                        try
                        {
                            Console.WriteLine("Digite o Nome do Usuário para ser excluido");
                            string NomeExluido = Console.ReadLine()!;
                            var UsuarioExcluido = usuarioDAL.RecuperarPor(e => e.Nome!.Equals(NomeExluido));

                            if (UsuarioExcluido != null)
                            {
                                usuarioDAL.Deletar(UsuarioExcluido);
                                Console.WriteLine("Usuário Exluído com Sucesso!");

                                Console.WriteLine("Digite uma tecla para voltar para o Menu Principal");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Usuário não encontrado");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Houve um erro Inesperado - {ex.Message}");
                        }
                        break;

                    case 5:
                        var usuarios = new Usuarios();
                        usuarios.ExportarClientetxt();
                        Console.WriteLine("Lista de Usuários Exportado com Sucesso no diretório: C:\\3- Estudos\\C#\\Clientes.txt");

                        Console.WriteLine("Digite uma tecla para voltar para o Menu Principal");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Opção não encontrada");
            }
        }
    }
}
