using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Entities
{
    public class Transaction : Entity<string>
    {
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public TransactionType.Type Type { get; set; }
        public Money Amount { get; set; }
        public Money BalanceAfter { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public string? ToAccountNumber { get; set; }
        public string? FromAccountNumber { get; set; }
       public Transaction(string id, string accountNumber, Money amount, TransactionType.Type type, string message)
            : base(id)
        {
            TransactionId = id;
            AccountNumber = accountNumber;
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.UtcNow;
            Description = message;
        }
        public override Entity<string> DeepClone()
        {
            return (Entity<string>) MemberwiseClone();
        }
        public override string ToString()
        {
            return $"TransactionId: {TransactionId}, AccountNumber: {AccountNumber}, Type: {Type}, Amount: {Amount}, " +
                   $"BalanceAfter: {BalanceAfter}, TransactionDate: {TransactionDate}, Description: {Description}, " +
                   $"Reference: {Reference}, ToAccountNumber: {ToAccountNumber}, FromAccountNumber: {FromAccountNumber}";
        }

    }
}
