using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Repositories
{
    public abstract class Repository<T,TID> : Domain.Interfaces.IRepository<T>  where T : Domain.Entities.Entity<TID> where TID : notnull 
    {
        public int Count => _dataStore.Count;
        private readonly Domain.Interfaces.IDataStore<T,TID> _dataStore;
        public Repository(Domain.Interfaces.IDataStore<T,TID> dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException (nameof(dataStore));
        }
        public virtual int Add(T entity)
        {
            return _dataStore.Add(entity);
        }
        public virtual int Update(T entity)
        {
            return _dataStore.Update(entity);
        }
        public virtual int Delete(object id)
        {
            TID typedId= (TID)id  ;
            return _dataStore.Delete(typedId);
        }
        public virtual T GetById(object id)
        {
            TID typedId = (TID)id;
            return _dataStore.Get(typedId);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _dataStore.GetAll();
        }
        public virtual bool Exists(TID id)
        {
            return _dataStore.Exists(id);
        }

    }
}
