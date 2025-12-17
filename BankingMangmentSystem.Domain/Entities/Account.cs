using BankingMangmentSystem.Domain._ُExceptions;
using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.Events;
using BankingMangmentSystem.Domain.Interfaces;
using BankingMangmentSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingMangmentSystem.Domain.Entities
{
    public abstract class Account : Entity<string>, Interfaces.IAccount
    {
        public string AccountNumber { get; protected set; }

        public Money Balance { get; protected set; }

        public AccountStatus.Status Status { get; protected set; }
        public int CustomerId { get; protected set; }
        public AccountType.Type AccountType { get; protected set; }
        public DateTime OpenedDate { get; protected set; }
        public DateTime? ClosedDate { get; protected set; }
        private List<Transaction> _transactions;
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
        public Transaction this[int index]
        {

            get => _transactions[index];
            protected set
            {
                if (index < 0 || index >= _transactions.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }
                _transactions[index] = value;
            }
        }
        public event Events.TransactionEventHandler? TransactionCompleted;
        public event Events.AccountEventHandler? StatusChanged;
        protected Account(string id, string AccountNumber, int CustomerId, AccountType.Type AccountType, int intialdeposit = 0) : base(id)
        {
            Balance = new Money(intialdeposit);
            this.AccountNumber = AccountNumber;
            this.CustomerId = CustomerId;
            this.AccountType = AccountType;
            Status = AccountStatus.Status.active;
            OpenedDate = DateTime.UtcNow;

        }
        public int CompareTo(IAccount? other)
        {
            return this.AccountNumber.CompareTo(other.AccountNumber);
        }

        public void Deposit(Money amount)
        {
            ValidateTransaction(amount);
            if (Status != AccountStatus.Status.active)
            {
                throw new AccountClosedException(AccountNumber);
            }
            Transaction transaction = new Transaction(Guid.NewGuid().ToString(), this.AccountNumber, amount, TransactionType.Type.deposit, "deposit");
            Balance += amount;
            _transactions.Add(transaction);
            SetUpdatedAt();
            TransactionCompleted(this, e: new Events.TransactionEventArgs(transaction.TransactionId, transaction.AccountNumber, transaction.Amount, transaction.Type, true, "done"));
        }
        public void Suspend(string reason)
        {
            if (reason != null)
            {
                StatusChanged(this, e: new Events.AccountEventArgs(AccountNumber,Status,AccountStatus.Status.suspended));
                Status = AccountStatus.Status.suspended;
            }
        }
        public abstract decimal CalculateInterest(int days);
        public void Activate()
        {
            StatusChanged(this, e: new Events.AccountEventArgs(AccountNumber, Status, AccountStatus.Status.active));
            Status = AccountStatus.Status.active;
            SetUpdatedAt();
        }
        public void Close()
        {
            if(Balance.Amount == 0)
            {
                StatusChanged(this, e: new Events.AccountEventArgs(AccountNumber, Status, AccountStatus.Status.closed));
                Status = AccountStatus.Status.closed;
                ClosedDate = DateTime.Now;
            }
        }
        protected void ValidateTransaction(Money amount)
        {
            if(amount.Amount <= 0)
            {
                throw new ArgumentException($"amount most be bigger than 0");
            }
            if(amount==null)
            {
                throw new ArgumentNullException(nameof(amount), "Transaction amount cannot be null");
            }
        }
        protected virtual void OnTransactionCompleted(Events.TransactionEventArgs e)
        {
            TransactionCompleted?.Invoke(this, e);
        }
        protected virtual void OnStatusChanged(AccountEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }
        public bool Equals(IAccount? other)
        {
            if(other == null)
                return false;
            return AccountNumber.Equals(other?.AccountNumber);
        }
        public override Entity<string> DeepClone()
        {
            throw new NotImplementedException("Derived classes must implement DeepClone()");
        }
        public virtual void Withdraw(Money amount)
        {
            ValidateTransaction(amount);
            if (Status != AccountStatus.Status.active)
            {
                throw new AccountClosedException(AccountNumber);
            }
            if (Balance.Amount < amount.Amount)
            {
                throw new InsufficientFundsException(amount.Amount, Balance.Amount);
            }
            Transaction transaction = new Transaction(Guid.NewGuid().ToString(), this.AccountNumber, amount, TransactionType.Type.withdrawal, "withdrawal");
            Balance -= amount;
            _transactions.Add(transaction);
            SetUpdatedAt();
            TransactionCompleted(this, e: new Events.TransactionEventArgs(transaction.TransactionId, transaction.AccountNumber, transaction.Amount, transaction.Type, true, "done"));
        }
        public void CloneDeepTransactions(Account account)
        {
            foreach(var transaction in _transactions)
            {
                var acc = (Transaction)transaction.DeepClone();
                account._transactions.Add(acc);
            }
        }
    }
}
