using BankingMangmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Extensions
{
    public static class CustomerExtensions
    {
        public static int GetTotalAccounts(this Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            return customer.Accounts.Count;
        }
        public static decimal GetTotalBalance(this Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            return customer.Accounts.Sum(a => a.Balance.Amount);
        }
        public static bool IsVIP(this Customer customer)
        {
            return customer.GetTotalBalance() >= 100000m;
        }
        public static string GetAgeGroup(this Customer customer)
        {
            int age = customer.Age;
            string ageGroup;
            switch (age)
            {
                case < 18:
                    ageGroup = "minor";
                    break;
                case < 30:
                    ageGroup = "Young Adult";
                    break;
                case < 50:
                    ageGroup = "Adult";
                    break;
                case < 65:
                    ageGroup = "Middle Age";
                    break;
                default:
                    ageGroup = "Senior";
                    break;
            }
            return ageGroup;
        }
    }
}
