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
    public class AccountRepository : Repository<Domain.Entities.Account,string>
    {
        public AccountRepository(Domain.Interfaces.IDataStore<Domain.Entities.Account,string> dataStore) : base(dataStore)
        {

        }
        public Account? GetCustomerByID(string CustomerID, out string error)
        {
            int? custID =Convert.ToInt32(CustomerID) ;
            if(custID == null)
            {
                error = ($"Invalid Customer ID {CustomerID}.");
                return null;
            }
            var account = GetAll().FirstOrDefault(x=> x.CustomerId == custID);

            if (account == null)
            {
                error = ($"Account with ID {CustomerID} not found.");
                return null;
            }
            error = string.Empty;
            return account;
        }
        public IEnumerable<Account> GetByStatus(AccountStatus status)
        {
                       return GetAll().Where(x => x.Status.Equals( status));
        }
        public IEnumerable<Account> GetByType(AccountType type)
        {
                       return GetAll().Where(x => x.AccountType.Equals(type));
        }
        public decimal GetTotalBalance()
        {
            return GetAll().Sum(x=>x.Balance.Amount);
        }
        public IEnumerable<Account> GetHighValueAccounts(decimal threshold)
        {
            return GetAll().Where(x => x.Balance.Amount > threshold);
        }
    }
}
