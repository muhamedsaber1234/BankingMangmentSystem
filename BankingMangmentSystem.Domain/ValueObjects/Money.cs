using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.ValueObjects
{
    public class Money : IEquatable<Money>, IComparable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public Money(decimal amount, string currency = "USD")
        {
            Amount = amount >= 0 ? amount : throw new ArgumentException("Amount cannot be negative", nameof(amount));
            Currency = currency ?? "USD";
        }

        public bool Equals(Money? other)
        {
            if (other == null) return false;
            return this.Amount == other.Amount && this.Currency == other.Currency;
        }

        public int CompareTo(Money? other)
        {
            if (other == null) return 1;
            if (this.Currency != other.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return this.Amount.CompareTo(other.Amount);
        }
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add amounts with different currencies.");
            return new Money(a.Amount + b.Amount, a.Currency);
        }
        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot deal with amounts with different currencies.");
            return new Money(a.Amount - b.Amount, a.Currency);
        }
        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount > b.Amount;
        }
        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount < b.Amount;
        }
        public static bool operator >=(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount >= b.Amount;
        }
        public static bool operator <=(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount <= b.Amount;
        }
        public static bool operator ==(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount == b.Amount;
        }
        public static bool operator !=(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot compare amounts with different currencies.");
            return a.Amount != b.Amount;
        }
        public override string ToString()
        {
            return $"you have {this.Amount:c}{this.Currency}";
        }
    }
}
