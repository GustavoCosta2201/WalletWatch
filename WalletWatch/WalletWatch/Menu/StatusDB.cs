using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Banco;

namespace WalletWatch.Menu
{
    internal class StatusDB
    {
        public static void VerificarStatusDB(ConnectionDB context)
        {
            Titulo.TituloMenu("Status do Banco de Dados");

            try
            {
                var connectionState = context.Database.GetDbConnection().State;

                if (connectionState == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Conexão com o banco de dados está aberta.");
                }
                else
                {
                    Console.WriteLine("Conexão com o banco de dados está fechada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao verificar o status do banco de dados: " + ex.Message);
            }
            Console.ReadLine(); 

        }
    }
}
