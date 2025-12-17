using BankingMangmentSystem.Domain._ُExceptions;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Entities
{
    public class SavingsAccount : Account
    {
        decimal InterestRate { get; set; } = 0.03m; 
        decimal MinimumBalance { get; set; } = 100; 
        public SavingsAccount(string id,string accnum,int customerID,Enums.AccountType.Type type= Enums.AccountType.Type.savings,int InitialDeposit=0,decimal interestRate=.03m, decimal MinimumBalance=100) : base( id,accnum,customerID, type, InitialDeposit)
        {
            interestRate = InterestRate;
            this.MinimumBalance = MinimumBalance;
        }
        public override decimal CalculateInterest(int days)
        {
            return Balance.Amount * InterestRate * days / 365;
        }
        public override void Withdraw(Money amount)
        {
            if(Balance.Amount - amount.Amount < MinimumBalance)
            {
                throw new InvalidTransactionException("Cannot withdraw: Minimum balance requirement not met.");
            }
            base.Withdraw(amount);
        }
        
    }
}
