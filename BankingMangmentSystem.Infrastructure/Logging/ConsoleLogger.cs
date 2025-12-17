using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingMangmentSystem.Domain.Interfaces;
namespace BankingMangmentSystem.Infrastructure.Logging
{
    public class ConsoleLogger : ITransactionLogger
    {
        public void Dispose()
        {

        }

        public void Log(string message)
        {
           Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {message}");
        }

        public void LogTransaction(object transaction)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {transaction}");
        }
    }
}
