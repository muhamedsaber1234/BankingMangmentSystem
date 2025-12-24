using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.Interfaces;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankingMangmentSystem.Domain.Enums.UserPermissions;

namespace BankingMangmentSystem.Domain.Entities
{
    public class Customer : Entity<string> ,IComparable<Customer>
    {
        string FirstName { get; init; }
        string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public UserPermission Permissions { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;
        private List<Account> _accounts;
        public IReadOnlyList<Account> Accounts => _accounts.AsReadOnly();
        public Customer(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, Address address, string id, UserPermission permissions = UserPermission.viewAccounts | UserPermission.deposit) : base(id)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DateOfBirth = dateOfBirth;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Permissions = permissions;
        }

        public override Entity<string> DeepClone()
        {
            var clone = new Customer(FirstName, LastName, DateOfBirth, Email, PhoneNumber, Address, Id, Permissions);
            foreach (var account in _accounts)
            {
                var clonedAccount = (Account)account.DeepClone();
                clone.AddAccount((Account)account);
            }
            return clone;
        }
        public void AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            _accounts.Add(account);
            SetUpdatedAt();
        }
        public bool HasPermission(UserPermission permission)
        {
            return (Permissions & permission) == permission;
        }
        public void GrantPermission(UserPermission permission)
        {
            Permissions |= permission;
            SetUpdatedAt();
        }
        public void RevokePermission(UserPermission permission)
        {
            Permissions &= ~permission;
            SetUpdatedAt();
        }

        public int CompareTo(Customer? other)
        {
            return this.FullName .CompareTo(other?.FullName);
        }
    }
}
