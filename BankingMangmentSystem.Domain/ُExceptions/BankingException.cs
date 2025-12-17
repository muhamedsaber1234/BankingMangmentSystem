using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain._ُExceptions
{
    public class BankingException : Exception
     {
        DateTime OccurredAt{ get; set; } 
        string ErrorCode { get; set; }
        public BankingException(string message ,string errorcode) 
        {
           ErrorCode = errorcode;
            OccurredAt = DateTime.UtcNow;
        }
        public BankingException(string message,Exception innerException, string errorCode)
        {
            ErrorCode = errorCode;
            OccurredAt = DateTime.UtcNow;
        }
     }
}
