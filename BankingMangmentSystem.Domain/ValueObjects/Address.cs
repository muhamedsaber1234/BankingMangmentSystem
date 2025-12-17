using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingMangmentSystem.Domain.ValueObjects
{
    public sealed class Address
    {
        string  Street  { get; } 
        string City    { get; }
        string State   { get; }
        string ZipCode { get; }
        string Country { get; }
        public Address(string street,string city,string state,string zipcodde,string country ="USA")
        {
            this.Street  = street;
            this.City    = city;
            this.State   = state;
            this.ZipCode = zipcodde;
            this.Country = country;
        }
        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {ZipCode}, {Country}";
        }
    }
}
