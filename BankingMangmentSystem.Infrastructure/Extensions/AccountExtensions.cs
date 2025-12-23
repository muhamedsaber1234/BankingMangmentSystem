using BankingMangmentSystem.Domain.Entities;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Extensions
{
    public static class AccountExtensions
    {
        public static string ToDetailedString(this Account account)
        {
            var s = new StringBuilder( $"AccountNumber: {account.AccountNumber}\n CustomerId: {account.CustomerId}\n AccountType: {account.AccountType}\n " +
                   $"Balance: {account.Balance}\n Status: {account.Status}\n CreatedAt: {account.CreatedAt}\n UpdatedAt: {account.UpdatedAt}");
            return s.ToString();
        }
        public static bool IsOverdrawn(this Account account)
        {
            return account.Balance.Amount < 0;
        }
        public static Money GetAvailableBalance(this Account account)
        {
            if (account is Domain.Entities.CheckingAccount acc)
            {
                return new Money(acc.Balance.Amount + acc.OverdraftLimit, acc.Balance.Currency);
            }
            else
            {
                return account.Balance;
            }
        }
    }
}
