using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Interfaces
{
    public interface IRepository<Tentity> where Tentity : class
    {
        int Add(Tentity entity);
        int Update(Tentity entity);
        int Delete(object id);
        Tentity? GetById(object id);
        IEnumerable<Tentity> GetAll();
    }
}
