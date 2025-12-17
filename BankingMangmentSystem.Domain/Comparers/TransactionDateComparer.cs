using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Comparers
{
    public class TransactionDateComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.CompareTo(y);
        }
    }
}
