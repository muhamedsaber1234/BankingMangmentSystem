using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Enums
{
    public class UserPermissions
    {
        [Flags]
        public enum UserPermission
        { 
            none = 0,
            viewAccounts = 1 ,
            deposit = 2,
            withdraw = 4,
            transfer = 8,
            mangeUsers = 16,
        }
    }
}
