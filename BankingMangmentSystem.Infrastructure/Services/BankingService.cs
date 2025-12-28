using BankingMangmentSystem.Domain._ُExceptions;
using BankingMangmentSystem.Domain.Entities;
using BankingMangmentSystem.Domain.Enums;
using BankingMangmentSystem.Domain.Events;
using BankingMangmentSystem.Domain.Interfaces;
using BankingMangmentSystem.Domain.ValueObjects;
using BankingMangmentSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static BankingMangmentSystem.Domain.Enums.UserPermissions;

namespace BankingMangmentSystem.Infrastructure.Services
{

    public class BankingService
    {
        CustomerRepository _customerRepository;
        AccountRepository _accountRepository;
        TransactionRepository _transactionRepository;
        ITransactionLogger _logger;
        public NotificationHandler? OnNotification;
        public InterestCalculator? InterestCalculators;
        public BankingService(CustomerRepository customerRepository, AccountRepository accountRepository, TransactionRepository transactionRepository, ITransactionLogger logger)
        {

            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(CustomerRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(AccountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(TransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(ITransactionLogger));
        }
        public event EventHandler<TransactionEventArgs>? TransactionProcessed;
        public event EventHandler<AccountEventArgs>? AccountCreated;
        public void CreateCustomer(string firstName, string lastName, string email, string phoneNumber,
           Address address, DateTime dateOfBirth, UserPermission permissions = default)
        {
            var customerToCheck = _customerRepository.GetByEmail(email);
            if (customerToCheck != null)
            {
                throw new BankingException($"the Email{email} is already exist", "Duplicated_mail");
            }
            string id = _customerRepository.Count.ToString();
            Customer customerToAdd = new Customer(firstName, lastName, dateOfBirth, email, phoneNumber, address, id, permissions);
            _customerRepository.Add(customerToAdd);
            _logger.LogTransaction(customerToAdd);
            OnNotification.Invoke($"Welcome {firstName} ");

        }
        public Customer? GetCustomer(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }
        public Account CreateAccount(int customerId, AccountType.Type type, int initialDeposit = 0, string? accountNumberPrefix = null)
        {
            var customer = _customerRepository.GetById(customerId);

            if (customer == null)
            {
                throw new BankingException($"user with account number {customerId} is not found", "Customer_NOT_Found");
            }
            string id;
            if (customer.HasPermission(UserPermission.deposit))
            {
                Money money = new Money(initialDeposit);
                Account acc = type switch
                {
                    AccountType.Type.Investment => new InvestmentAccount(0, 0, null, null, customerId, type, initialDeposit),
                    AccountType.Type.savings => new SavingsAccount(null, null, customerId, type, initialDeposit),
                    AccountType.Type.checking => new CheckingAccount(null, null, customerId, type, initialDeposit),
                    _ => throw new BankingException($"Account type {type} is not supported", "Account_Type_Not_Supported"),
                };
                acc.TransactionCompleted += OnTransactionCompleted;
                acc.StatusChanged += OnAccountStatusChanged;
                _accountRepository.Add(acc);
                customer.AddAccount(acc);
                _logger.LogTransaction(acc);
                AccountCreated(this, new AccountEventArgs(acc.AccountNumber, AccountStatus.Status.pending, AccountStatus.Status.active));
                return acc;
            }
            return null;
        }
        public void OnTransactionCompleted(object? sender, TransactionEventArgs e)
        {
            _logger.LogTransaction($"Transaction on account: {e.AccountNumber}, " +
             $"Type: {e.Type}, Amount: {e.Amount}");
        }
        public void OnAccountStatusChanged(object? sender, AccountEventArgs e)
        {            
            _logger.LogTransaction($"Account status changed for account: {e.AccountNumber}, " +
             $"New Status: {e.NewStatus}");
        }
        public Account? GetAccount(string accountNumber)
        {
            return _accountRepository.GetAll().FirstOrDefault( x => x.AccountNumber == accountNumber);
        }
        public void Deposit(string accountNumber, decimal amount, string? description = null)
        {
           var acc =  GetAccountORThrow(accountNumber);
            Money money = new Money(amount);
            acc.Deposit(money);
            Transaction transaction = new Transaction(Guid.NewGuid().ToString(), accountNumber, money, TransactionType.Type.deposit, description ?? "deposit");
            _transactionRepository.Add(transaction);
            _logger.LogTransaction(transaction);
            TransactionProcessed(this,new TransactionEventArgs( acc.Id, accountNumber,money, TransactionType.Type.deposit,true, "Transaction Processed"));
            
        }
        public void Withdraw(string accountNumber, decimal amount, string? description = null)
        {
            var acc = GetAccountORThrow(accountNumber);
            Money money = new Money(amount);
            acc.Withdraw(money);
            Transaction transaction = new Transaction(Guid.NewGuid().ToString(), accountNumber, money, TransactionType.Type.withdrawal, description ?? "withdraw");
            _transactionRepository.Add(transaction);
            _logger.LogTransaction(transaction);
            TransactionProcessed(this, new TransactionEventArgs(acc.Id, accountNumber, money, TransactionType.Type.withdrawal, true, "Transaction Processed"));
        }
        public void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, out string transactionId, ref int transferCount)
        {
            var accFROM = GetAccountORThrow(fromAccountNumber);
            var accTO = GetAccountORThrow(toAccountNumber);
            Money money = new Money(amount);
            accFROM.Withdraw(money);
            accTO.Deposit(money);
            Transaction transaction = new Transaction(Guid.NewGuid().ToString(), fromAccountNumber, money, TransactionType.Type.withdrawal, "Transfer");
            transaction.ToAccountNumber = toAccountNumber;
            transaction.FromAccountNumber = fromAccountNumber;
            transactionId = transaction.TransactionId;
            transferCount++;
            _transactionRepository.Add(transaction);
            _logger.LogTransaction(transaction);
            OnNotification?.Invoke($"Transfer of {amount} from {fromAccountNumber} to {toAccountNumber} completed.");
        }

        public Account? GetAccountORThrow(string accountNumber)
        {
                       var acc = GetAccount(accountNumber);
            if (acc == null)
            {
                throw new BankingException($"Account with ID {acc.Id} not found.", "Account_NOT_Found");
            }
            return acc;
        }
        public IEnumerable<Transaction>? GetAccountStatement(string accountNumber, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var accFROM = GetAccountORThrow(accountNumber);
           return _transactionRepository.GetAll().Where(t =>
                t.AccountNumber == accountNumber &&
                (  t.TransactionDate >= (fromDate??=DateTime.Now.AddMonths(-1))) &&
                ( t.TransactionDate <= (toDate??=DateTime.Now))).OrderByDescending(t=>t.TransactionDate);
        }
       public  Dictionary<AccountType.Type, decimal> GetBalancesByAccountType()
        {
            var accounts = _accountRepository.GetAll();
            var balancesByType = accounts
                .GroupBy(acc => acc.AccountType)
                .ToDictionary(
                    group => group.Select(x =>x.AccountType).First(),
                    group => group.Sum(acc => acc.Balance.Amount)
                );
            return balancesByType;
        }
        public decimal CalculateTotalBalance(params string[] accountNumbers)
        {
            decimal totalBalance = 0;
            if (accountNumbers == null || accountNumbers.Length == 0)
            {
               return totalBalance;
            }
            foreach (var accNum in accountNumbers)
            {
                var account = GetAccountORThrow(accNum);
                totalBalance += account.Balance.Amount;
            }
            return accountNumbers.Select(a=> _accountRepository.GetAll().FirstOrDefault(x=>x.AccountNumber == a)).
                Where(acc => acc != null).
                Sum(a=>a.Balance.Amount);
        }
        public void ApplyInterestToAccounts(InterestCalculator calculator, int days = 30)
        {
            var accounts = _accountRepository.GetAll().Where(x => x.Status == AccountStatus.Status.active);
            foreach (var account in accounts)
            {
                decimal interest = calculator(account.Balance.Amount, days);
                if (interest > 0)
                {
                    account.Deposit(new Money(interest));
                    _logger.LogTransaction($"Applied interest of {interest} to account {account.AccountNumber} for {days} days.");
                }
            }
        }
        public object GetBankStatistics()
        {
            int totalCustomers = _customerRepository.Count;
            int totalAccounts = _accountRepository.Count;
            int TotalBalance = _accountRepository.GetAll().Sum(a => (int)a.Balance.Amount);
            decimal AvarageBalancePerAccount = _accountRepository.GetAll().Any() ? _accountRepository.GetAll().Average(x=>x.Balance.Amount):0;
            int totalTransactions = _transactionRepository.Count;
            int TotalAccountsByType = _accountRepository.GetAll().GroupBy(a => a.AccountType).Count();
            return new
            {
                TotalCustomers = totalCustomers,
                TotalAccounts = totalAccounts,
                TotalBalance = TotalBalance,
                AvarageBalancePerAccount = AvarageBalancePerAccount,
                TotalTransactions = totalTransactions,
                TotalAccountsByType = TotalAccountsByType,
                topCustomers = _customerRepository.GetAll()
                    .OrderByDescending(c => c.Accounts.Sum(a => a.Balance.Amount))
                    .Take(5)
                    .Select(c => new
                    {
                        CustomerId = c.Id,
                        Name = $"{c.FullName} ",
                        TotalBalance = c.Accounts.Sum(a => a.Balance.Amount)
                    })
            };
        }
    }
}
