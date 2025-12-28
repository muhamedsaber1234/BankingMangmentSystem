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
        public string AccountNumber    { get; set; }
        public AccountStatus.Status OldStatus { get; set; }
        public AccountStatus.Status NewStatus { get; set; }
        public DateTime Timestamp      { get; set; }
        public AccountEventArgs(string accnum, AccountStatus.Status oldStatus, AccountStatus.Status newStatus)
        {
            AccountNumber= accnum;
            OldStatus= oldStatus;
            NewStatus= newStatus;
            Timestamp= DateTime.UtcNow;
        }
    }
}
