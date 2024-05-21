using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;
using WalletWatch.Modelos;
using OpenAI_API;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml;


namespace WalletWatch.Menu
{
    internal class GerenciarTransacoes
    {

        public static void GerenciarTransacao()
        {
            var context = new ConnectionDB();
            var usuarioDAL = new DAL<Usuarios>(context);
            var categoriaDAL = new DAL<Categorias>(context);
            var transacaoDAL = new DAL<Transacoes>(context);

            Console.Clear();

            Titulo.TituloMenu("Gerenciador de Transações");

            Dictionary<int, string> opcaoTransaction = new Dictionary<int, string>()
            {
                {1, "Cadastrar Transação" },
                {2, "Atualizar Transação" },
                {3, "Remover Transação" },
                {4, "Listar Transações" },
                {5, "Exportar Transações" },
                {6, "Pesquisar Transações" },
                {7, "Sair" }

            };

            foreach (var transaction in opcaoTransaction)
            {
                Console.WriteLine($"{transaction.Key} - {transaction.Value}");
            }

            int opcao = LeituraEntrada.LerOpcao();

            switch (opcao)
            {
                case 1:
                    bool sair = true;
                    while (sair)
                    {
                        var transaction = new Transacoes();

                        Console.WriteLine("Digite o Nome do Usuário");
                        string usuario = Console.ReadLine()!;

                        var usuarioRecuperado = usuarioDAL.RecuperarPor(u => u.Nome!.Equals(usuario));

                        if (usuarioRecuperado != null)
                        {
                            int idUsuario = usuarioRecuperado.Id_Usuario;
                            transaction.Id_Usuario = idUsuario;

                            Console.WriteLine("Digite a categoria da transação");
                            string categoria = Console.ReadLine()!;

                            var categoriaRecuperado = categoriaDAL.RecuperarPor(u => u.Nome!.Equals(categoria));

                            if (categoriaRecuperado != null)
                            {
                                int idCategoria = categoriaRecuperado.Id_Categoria;
                                transaction.Id_Categoria = idCategoria;

                                Console.WriteLine("Digite a descrição da transação: ");
                                string descricao = Console.ReadLine()!;
                                transaction.Descricao = descricao;

                                Console.WriteLine("Digite o valor da transação");
                                string valor = Console.ReadLine()!;
                                transaction.Valor = int.Parse(valor);

                                transacaoDAL.Adicionar(transaction);
                                Console.WriteLine("Transação cadastrada com sucesso!");
                                sair = false;

                            }
                            else
                            {
                                Console.WriteLine("Categoria não encontrada! Tente novamente.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usuário não encontrado! Tente novamente.");
                        }
                    }
                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;


                case 2:
                    Console.WriteLine("Digite o ID da Transação para atualizar:");
                    int IDTransaction = int.Parse(Console.ReadLine()!);

                    transacaoDAL = new DAL<Transacoes>(context);
                    var transactionRecuperado = transacaoDAL.RecuperarPor(i => i.Id_Transacao.Equals(IDTransaction));

                    if (transactionRecuperado != null)
                    {
                        Console.WriteLine("Digite a nova descrição da transação: ");
                        transactionRecuperado.Descricao = Console.ReadLine();

                        Console.WriteLine("Digite o novo valor da transação:");
                        transactionRecuperado.Valor = int.Parse(Console.ReadLine()!);

                        transacaoDAL.Atualizar(transactionRecuperado);
                        Console.WriteLine("Atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("ID da transação não encontrado!");
                    }
                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Digite o ID da transação para remover:");
                    int ID = int.Parse(Console.ReadLine()!);

                    transacaoDAL = new DAL<Transacoes>(context);

                    var Recuperar = transacaoDAL.RecuperarPor(r => r.Id_Transacao.Equals(ID));

                    if (Recuperar != null)
                    {
                        transacaoDAL.Deletar(Recuperar);
                        Console.WriteLine("Removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("ID da transação não encontrado!");
                    }

                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 4:
                    var transactionList = new Transacoes();
                    transactionList.ListarTransacoes();
                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;

                case 5:
                    transactionList = new Transacoes();
                    transactionList.ExportarTransacoestxt();
                    Console.WriteLine("\nDigite uma tecla para voltar para o Menu Principal");
                    Console.ReadKey();
                    break;


                case 6:
                    Console.Write("Digite o nome do usuário: ");
                    string nome = Console.ReadLine()!;

                    var result = PesquisarPorCliente(nome);
                    DadosRetornados(result);
                    Console.ReadKey(); 
                    break;

                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }

        }
        public static IEnumerable<Transacoes> PesquisarPorCliente(string Nome)
        {
            var context = new ConnectionDB();
            var transacao = new DAL<Transacoes>(context);
            var cliente = new DAL<Usuarios>(context);

            var recuperarCliente = cliente.RecuperarPor(c => c.Nome!.Equals(Nome));

            if (recuperarCliente != null)
            {
                var id = recuperarCliente.Id_Usuario;
                var lista = transacao.Listar();

                return lista.Where(t => t.Id_Usuario.Equals(id));
            }
            else
            {
                Console.WriteLine("Cliente não encontrado!");
                return Enumerable.Empty<Transacoes>(); // Retorna uma coleção vazia se o cliente não for encontrado
            }
        }


        public static void DadosRetornados(IEnumerable<Transacoes> transacoes)
        {
            if (transacoes != null)
            {
                
                foreach (var transacao in transacoes)
                {
                    Console.WriteLine($"\n{transacao.Id_Transacao}\t - {transacao.Descricao}\t - {transacao.Valor}\t");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma transação encontrada.");
            }
        }

    }
}



