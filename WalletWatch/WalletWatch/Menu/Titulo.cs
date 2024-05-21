using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletWatch.Menu
{
    public class Titulo
    {
        public static void TituloMenu(string titulo)
        {
            Console.Clear();
            int tamanho = titulo.Length;
            string igual = string.Empty.PadLeft(titulo.Length, '=');
            Console.WriteLine(igual);
            Console.WriteLine(titulo);
            Console.WriteLine($"{igual} \n");
            
        }
    }
}
