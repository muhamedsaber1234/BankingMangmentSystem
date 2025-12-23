using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Domain.Entities.Customer,string>
    {

        public CustomerRepository(Domain.Interfaces.IDataStore<Domain.Entities.Customer,string> dataStore) : base(dataStore)
        {
            
        }
        public Domain.Entities.Customer? GetByEmail(string email )
        {
            var Customer = GetAll().FirstOrDefault(x=> x.Email ==email);
            if(Customer == null)
            {
                return null;
            }
            return Customer;
        }
        public IEnumerable<Domain.Entities.Customer?> GetByPermission(Domain.Enums.UserPermissions Permission)
        {
            return GetAll().Where(x=> x.Permissions.Equals(Permission));
        }
        public IEnumerable<Domain.Entities.Customer?> SearchByName(string name)
        {
            return GetAll().Where(x => x.FullName.Contains(name,StringComparison.OrdinalIgnoreCase));
        }

    }
}
