using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingMangmentSystem.Domain._ُExceptions
{
    public class InsufficientFundsException : BankingException
    {
        public decimal RequestedAmount { get; set; }
        public decimal AvailableBalance { get; set; }
        public InsufficientFundsException(decimal requestedAmount, decimal availableBalance)
            : base($"Insufficient funds: Requested {requestedAmount}, Available {availableBalance}", "INSUFFICIENT_FUNDS")
        {
            RequestedAmount = requestedAmount;
            AvailableBalance = availableBalance;
        }
    }
}
