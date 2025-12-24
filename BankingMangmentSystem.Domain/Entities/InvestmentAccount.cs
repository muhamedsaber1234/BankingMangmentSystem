using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Entities
{
    public class InvestmentAccount : Account
    {
   

        decimal RiskFactor { get; set; }
        decimal BaseInterestRate {  get; set; }

        public InvestmentAccount(decimal riskFactor, decimal baseInterestRate,string id,string accNum,int custID,Enums.AccountType.Type type,int deposit) :base(id, accNum, custID,type,deposit)
        {
            RiskFactor = riskFactor;
            BaseInterestRate = baseInterestRate;
        }

        public override decimal CalculateInterest(int days)
        {

         return Balance.Amount* BaseInterestRate *RiskFactor * days / 365;
        }
    }
}
