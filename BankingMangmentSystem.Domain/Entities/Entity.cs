using BankingMangmentSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; }

        public DateTime CreatedAt { get;  }

        public DateTime? UpdatedAt { get; private set; }
        public Entity(T id)
        {
            Id= id;
            CreatedAt = DateTime.UtcNow;
        }
        protected void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public object Clone()
        {
            return (Entity < T > )MemberwiseClone();
        }
        public abstract Entity<T> DeepClone();
    }
}
