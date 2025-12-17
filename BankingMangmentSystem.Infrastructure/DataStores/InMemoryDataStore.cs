using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.DataStores
{
    public class InMemoryDataStore<T,TID> : Domain.Interfaces.IDataStore<T, TID> where T : Domain.Entities.Entity<TID> where TID : notnull
    {
        private readonly ConcurrentDictionary<TID, T> _store = new();
        public int Count => _store.Count;

        public int Add(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (_store.ContainsKey(entity.Id))
            {
                if (_store.TryAdd(entity.Id, entity))
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }

        public int Delete(TID id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (_store.ContainsKey(id))
            {
                _store.TryRemove(id,out T? value);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public bool Exists(TID id)
        {
            if (id == null) {
                throw new ArgumentNullException(nameof(id));
            }
            return _store.ContainsKey(id);
        }

        public T Get(TID id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (_store.TryGetValue(id, out var entity))
            {
                return entity;
            }
            else
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _store.Values.ToList();
        }

        public int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (_store.ContainsKey(entity.Id))
            {
                T comperObject = _store[entity.Id];
                if (_store.TryUpdate(entity.Id, entity, comperObject))
                    return 1;
                else 
                    return 0;
            }
            else
            {
                return 0;
            }
        }

    }
}
