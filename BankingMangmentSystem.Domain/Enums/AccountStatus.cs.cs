using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Enums
{
    public class AccountStatus
    {
        public enum Status
        {
            pending,
            active,
            suspended,
            closed
        }
    }
}
