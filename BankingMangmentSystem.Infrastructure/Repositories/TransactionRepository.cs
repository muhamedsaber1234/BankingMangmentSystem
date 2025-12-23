using BankingMangmentSystem.Domain.Entities;
using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankingMangmentSystem.Infrastructure.Repositories
{

    public class TransactionRepository : Repository<Domain.Entities.Transaction,string>
    {
        public TransactionRepository(IDataStore<Transaction,string> datastore):base(datastore)
        {
            
        }
        public IEnumerable<Transaction> GetByAccountNumber(string accountNumber)
        { 
        return GetAll().Where(x=>x.AccountNumber == accountNumber);
        }
        public IEnumerable<Transaction> GetByDateRange(DateTime from, DateTime to)
        {
            return GetAll().OrderBy(x=>x.TransactionDate).Where(x => x.TransactionDate >= from && x.TransactionDate <= to);
        }
        public IEnumerable<Transaction> GetByType(TransactionType type)
        {
            return GetAll().Where(x=>x.Type.Equals(type));
        }
        public decimal GetTotalByType(TransactionType type, DateTime? from = null)
        {
            var Transactions = GetAll().Where(x => x.Type.Equals(type));
            if (from.HasValue)
            {
                Transactions = Transactions.Where(x => x.TransactionDate >= from.Value);
            }
            return Transactions.Sum(x => x.Amount.Amount);
        }
    }
}
