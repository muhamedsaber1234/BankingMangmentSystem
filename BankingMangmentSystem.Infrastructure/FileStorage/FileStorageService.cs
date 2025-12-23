using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization.Metadata;
namespace BankingMangmentSystem.Infrastructure.FileStorage
{
    public class FileStorageService
    {
        readonly string  _basePath;

        public FileStorageService(string basePath)
        {
            if (basePath == null)
            { 
            throw new ArgumentNullException(basePath);
            }
            _basePath = basePath;

            string path =Path.GetDirectoryName(_basePath) ?? string.Empty;
            if(!Directory.Exists(_basePath) && !string.IsNullOrEmpty(_basePath))
            {
                               Directory.CreateDirectory(_basePath);
            }
        }

        public void Save<T>(T data, string fileName)
        {
            try
            {
                string fullPath = Path.Combine(_basePath, fileName);
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonData = System.Text.Json.JsonSerializer.Serialize(data, options);
                File.WriteAllText(fullPath, jsonData);
            }
            catch (Exception ex)
            {
                throw new BankingMangmentSystem.Domain._ُExceptions.BankingException(ex.Message, "FILE_SAVE_ERROR");
            }
            finally
            { }
        }
        public T? Load<T>(string fileName) where T : class
        {
            try
            {
                string fullPath = Path.Combine(_basePath, fileName);
                if (!File.Exists(fullPath))
                {
                    return null;
                }
                string jsonData = File.ReadAllText(fullPath);
                T? data = System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);
                return data;
            }
            catch (Exception ex)
            {
                throw new BankingMangmentSystem.Domain._ُExceptions.BankingException(ex.Message, "FILE_LOAD_ERROR");
            }
            finally
            {
                
            }
        }

    }
}
