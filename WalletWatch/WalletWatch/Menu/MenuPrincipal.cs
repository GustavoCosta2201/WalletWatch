using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;

namespace WalletWatch.Menu
{
    internal class MenuPrincipal
    {
        static ConnectionDB context = new ConnectionDB();
        public static void MainMenu()
        {
            bool sair = true;
            while (sair)
            {
                Titulo.TituloMenu("Bem-Vindo ao WalletWatch. Gerenciamento de Finanças");
                Console.WriteLine("Digite uma Opção abaixo:");

                Dictionary<int, string> opcoesMenu = new Dictionary<int, string>()
                    {

                        {1, "Gerenciar Usuários" },
                        {2, "Gerenciar Categorias" },
                        {3, "Gerenciar Transações" },
                        {4, "Status Banco de Dados" },
                        {5, "Sair" }

                    };

                foreach (var item in opcoesMenu)
                {
                    Console.WriteLine($"{item.Key} - {item.Value}");
                }

                int opcaoPrincipal = LeituraEntrada.LerOpcao();
                switch (opcaoPrincipal)
                {
                    case 1:
                        GerenciarUsuarios.GerenciarUsuario();
                        break;
                    case 2:
                        GerenciarCategorias.GerenciarCategoria();
                        break;
                    case 3:
                        GerenciarTransacoes.GerenciarTransacao();
                        break;
                    case 4:
                        StatusDB.VerificarStatusDB(context);
                        break;
                    case 5:
                        sair = false;
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        break;
                }
            }
        }
    }
}
