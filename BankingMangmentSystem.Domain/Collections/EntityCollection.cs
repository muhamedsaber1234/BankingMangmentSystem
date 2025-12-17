using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Collections
{
    public class EntityCollection<t> where t : Entities.Entity<string>
    {
        Dictionary<object, t> _items;
        int Count => _items.Count;
        public EntityCollection()
        {
            _items = new Dictionary<object, t>();
        }
        public void Add(t item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (_items.ContainsKey(item.Id))
                throw new ArgumentException($"An item with the same ID already exists: {item.Id}");

            _items[item.Id] = item;
        }
        public t? Get(object id)
        {
            return _items[id];
        }
        public bool Remove(object id)
        {
            return _items.Remove(id);
        }
        public IEnumerable<t> GetAll()
        {
            return _items.Values;
        }
        public void Clear()
        {
            _items.Clear();
        }
    }
}
