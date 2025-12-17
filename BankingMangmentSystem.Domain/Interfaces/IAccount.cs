using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Interfaces
{
    public interface IAccount : IComparable<IAccount>, IEquatable<IAccount>
    {
        string AccountNumber { get; }
        Money Balance { get; }
        Enums.AccountStatus.Status Status { get; }
        void Deposit(Money amount);
        void Withdraw(Money amount);
    }
}
