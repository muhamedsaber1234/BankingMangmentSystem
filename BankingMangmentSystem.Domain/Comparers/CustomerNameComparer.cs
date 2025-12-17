using BankingMangmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Comparers
{
    public class CustomerNameComparer : IComparer<Entities.Customer>
    {
        public int Compare(Customer? x, Customer? y)
        {
           if (x == null && y == null) return 0;
              if (x == null) return -1;
                if (y == null) return 1;
                return string.Compare(x.FullName, y.FullName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
