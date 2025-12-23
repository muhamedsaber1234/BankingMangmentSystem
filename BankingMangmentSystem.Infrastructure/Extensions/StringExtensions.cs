using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string MaskAccountNumber(this string? accountNumber)
        {
            if (accountNumber == null || accountNumber.Length < 4)
            {
                return accountNumber;
            }
            else
            {
                return new string('*',accountNumber.Length-4)+ accountNumber[^4..];
            }
        }
        
    }
}
