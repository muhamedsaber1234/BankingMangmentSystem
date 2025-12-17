using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Interfaces
{
    public interface IRepository<Tentity> where Tentity : class
    {
        void Add(Tentity entity);
        void Update(Tentity entity);
        void Delete(Tentity entity);
        Tentity? GetById(object id);
        IEnumerable<Tentity> GetAll();
    }
}
