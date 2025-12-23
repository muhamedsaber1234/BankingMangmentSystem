using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Entities
{
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit{ get; set; }
        public decimal MonthlyFee { get; set; }
        public CheckingAccount(string id, string accnum, int customerID, Enums.AccountType.Type type = Enums.AccountType.Type.checking, int InitialDeposit = 0, decimal overdraftLimit = 500, decimal monthlyFee = 10) : base(id, accnum, customerID, type, InitialDeposit)
        {
            OverdraftLimit = overdraftLimit;
            MonthlyFee = monthlyFee;
        }
        public override void Withdraw(Money amount)
        {
            if (Balance.Amount - amount.Amount < -OverdraftLimit)
            {
                throw new _ُExceptions.InvalidTransactionException("Cannot withdraw: Overdraft limit exceeded.");
            }
            base.Withdraw(amount);
        }

        public override decimal CalculateInterest(int days)
        {
            return 0;
        }
    }
}
