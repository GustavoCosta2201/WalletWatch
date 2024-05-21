using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletWatch.Menu
{
    internal class LeituraEntrada
    {
        public static int LerOpcao()
        {
            Console.Write("Digite uma opção:\n");
            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção Inválida. Tente Novamente\n");
                Console.WriteLine("Escolha uma opção\n");
            }
            return opcao;
        }
    }
}
