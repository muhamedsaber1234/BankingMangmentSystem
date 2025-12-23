using BankingMangmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Account> ActiveOnly(this IEnumerable<Account> accounts)
        {
            return accounts.Where(x => x.Status == Domain.Enums.AccountStatus.Status.active);
        }
        public static IEnumerable<Account> WithMinimumBalance(this IEnumerable<Account> accounts, decimal minimum)
        {
            return accounts.Where(x => x.Balance.Amount >= minimum);
        }

    }
}
