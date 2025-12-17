using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Events
{
    public class TransactionEventArgs : EventArgs
    {
        string TransactionId { get; set; }
        string AccountNumber { get; set; }
        Money Amount         { get; set; }
        TransactionType.Type Type { get; set; }
        DateTime Timestamp   { get; set; }
        bool Success         { get; set; }
        string Message       { get; set; }
        public TransactionEventArgs(string id,string accnum,Money amount,TransactionType.Type transactionType, bool success,
            string message)
        {
            TransactionId= id;
            AccountNumber= accnum;
            Amount= amount;
            Success= success;
            Message= message;
            Type = transactionType;
            Timestamp= DateTime.UtcNow;
        }
    }
}
