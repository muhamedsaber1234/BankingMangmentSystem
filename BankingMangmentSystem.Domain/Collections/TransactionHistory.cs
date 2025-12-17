using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Collections
{
    public class TransactionHistory
    {
        private Stack<Entities.Transaction> _transactions;
        private int count;
        public TransactionHistory()
        {
            _transactions = new Stack<Entities.Transaction>();
        }
        public void Push(Entities.Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            _transactions.Push(transaction);
            count++;
        }
        public Entities.Transaction? Pop()
        {
            if (_transactions.Count == 0)
                return null;
            count--;
            return _transactions.Pop();
        }
        public Entities.Transaction? Peek()
        {
            if (_transactions.Count == 0)
                return null;
            return _transactions.Peek();
        }
        public IEnumerable<Entities.Transaction> GetAll()
        {
            return _transactions.ToList();
        }
    }
}
