using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Events
{
    public delegate void TransactionEventHandler(object sender, TransactionEventArgs e);
    public delegate void AccountEventHandler(object sender, AccountEventArgs e);
    public delegate void NotificationHandler(string message);
    public delegate decimal InterestCalculator(decimal balance, int days);
}
