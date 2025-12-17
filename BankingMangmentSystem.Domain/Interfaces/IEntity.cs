using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Interfaces
{
    public interface IEntity<T> : ICloneable
    {
        T Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
