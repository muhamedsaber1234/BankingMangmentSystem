using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Interfaces
{
    public interface IDataStore<T,TID> where T : Entities.Entity<TID> where TID :notnull
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(TID id);
        T Get(TID id);
        IEnumerable<T> GetAll();
        bool Exists(TID id);
        int Count{ get; }
    }
}
