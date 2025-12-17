using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain._ُExceptions
{
    public class InvalidTransactionException : BankingException
    {
        public InvalidTransactionException( string msg):base(msg, "INVALID_TRANSACTION")
        {
            
        }
    }
}
