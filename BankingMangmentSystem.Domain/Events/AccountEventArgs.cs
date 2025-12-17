using BankingMangmentSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Events
{
    public class AccountEventArgs : EventArgs
    {
        string AccountNumber    { get; set; }
        AccountStatus.Status OldStatus { get; set; }
        AccountStatus.Status NewStatus { get; set; }
        DateTime Timestamp      { get; set; }
        public AccountEventArgs(string accnum, AccountStatus.Status oldStatus, AccountStatus.Status newStatus)
        {
            AccountNumber= accnum;
            OldStatus= oldStatus;
            NewStatus= newStatus;
            Timestamp= DateTime.UtcNow;
        }
    }
}
