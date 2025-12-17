using BankingMangmentSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Infrastructure.Logging
{
    public class FileLogger : ITransactionLogger
    {
        string _logFilePath;
        StreamWriter? _writer;
        bool _disposed;
        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
            string directory = Path.GetDirectoryName(_logFilePath) ?? string.Empty;
            if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            _writer = new StreamWriter(logFilePath, append: true);
        }
        public void Dispose()
        {
                 Dispose(true);
              GC.SuppressFinalize(this);
        }
        ~FileLogger() { Dispose(false); }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_writer != null)
                    {
                        _writer?.Flush();
                        _writer?.Close();
                        _writer?.Dispose();
                        _writer = null;
                    }
                }
            }
            _disposed = true;
        }
        public void Log(string message)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FileLogger));
            }
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {message}";
            _writer?.WriteLine(logEntry);
            _writer?.Flush();
        }

        public void LogTransaction(object transaction)
        {
            Log($"Transaction Logged: {transaction.ToString()}");
        }
        public void LogException(Exception ex)
        {
            StringBuilder sb = new StringBuilder($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\nException type:{ex.GetType()}");
            if (ex.InnerException != null)
            { 
                sb.AppendLine($"Inner Exception:{ex.InnerException}");
            }
            Log(sb.ToString());
        }
    }
}
