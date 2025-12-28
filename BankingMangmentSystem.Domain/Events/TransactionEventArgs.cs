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
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public Money Amount         { get; set; }
        public TransactionType.Type Type { get; set; }
        public DateTime Timestamp   { get; set; }
        public bool Success         { get; set; }
        public string Message       { get; set; }
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
