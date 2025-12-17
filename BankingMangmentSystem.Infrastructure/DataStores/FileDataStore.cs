using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BankingMangmentSystem.Infrastructure.DataStores
{
    public class FileDataStore<T,TID> :Domain.Interfaces.IDataStore<T,TID> where T : Domain.Entities.Entity<TID> where TID : notnull
    {
        private readonly ConcurrentDictionary<TID, T> _store = new ();
        public int Count => _store.Count;
        readonly string _path;
        readonly string _FileName;

        public FileDataStore(string filePath,string fileName) 
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory == null)
            {
                throw new ArgumentException(filePath);
            }
            if(!Directory.Exists(directory) && !string.IsNullOrEmpty(filePath))
            {
                Directory.CreateDirectory(directory);
            }
            if(fileName == null)
            {
                throw new ArgumentException(fileName);
            }
            _path = filePath;
            _FileName = fileName;
            LoadFromFile();
        }
        private void LoadFromFile()
        {
            _store.Clear ();
            string fullFilePath = Path.Combine(_path,_FileName);
            if (File.Exists(fullFilePath))
            {
               string data = File.ReadAllText(fullFilePath);
                if (data != null)
                {
                    var JSonData = JsonSerializer.Deserialize<Dictionary<TID, T>>(data);
                    foreach(var T in JSonData)
                    {
                        _store[T.Key] = T.Value;
                    }
                }
            }

        }
        public int Add(T entity)
        {
            if(entity == null)
            {
                return -1;
            }
            
            return Convert.ToInt32(_store.TryAdd(entity.Id, entity));
        }

        public int Delete(TID id)
        {
            if(id == null)
                { return -1; }
            T s;
            int x = Convert.ToInt32(_store.TryRemove(_store[id].Id, out s));
            SaveToFile();
            return x;
        }

        public bool Exists(TID id)
        {
            if (id == null)
            throw new ArgumentNullException("id");

            return _store.ContainsKey(id);
        }

        public T Get(TID id)
        {
            if( id == null)
                throw new ArgumentNullException("id");
            return _store[id];
        }

        public IEnumerable<T> GetAll()
        {
            return _store.Values;
        }

        public int Update(T entity)
        {
            T CompareValue = _store[entity.Id];
                
            if (entity == null|| CompareValue == null)
                throw new ArgumentNullException("id");
            if (!_store.ContainsKey(entity.Id)) { return -1; }
            else if (_store.TryUpdate(entity.Id, entity, CompareValue))
                return 1;
            else
                return 0;
        }
        private void SaveToFile()
        {
            string fu = Path.Combine(_path,_FileName);
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            string jsonData = System.Text.Json.JsonSerializer.Serialize(_store.ToDictionary(x =>x.Key,x =>x.Value), options);
            File.WriteAllText(fu, jsonData);
        }
    }
}
