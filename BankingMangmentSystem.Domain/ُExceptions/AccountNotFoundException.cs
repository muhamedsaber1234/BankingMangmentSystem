using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain._ُExceptions
{
    public class AccountNotFoundException : BankingException
    {
        string AccountNumber { get; set; }
        public AccountNotFoundException(string accountNumber):base($"there is no account with this id :{accountNumber}", "ACCOUNT_NOT_FOUND")
        {
            AccountNumber = accountNumber;
        }
    }
}
