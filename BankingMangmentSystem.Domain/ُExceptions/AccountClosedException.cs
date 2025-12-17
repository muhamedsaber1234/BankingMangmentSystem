using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain._ُExceptions
{
    public class AccountClosedException : BankingException
    {
        string AccountNumber { get; set; }
        public AccountClosedException(string accNum):base($"The account with number {accNum} is closed and cannot perform transactions.", "ACCOUNT_CLOSED")
        {
            
        }
    }
}
