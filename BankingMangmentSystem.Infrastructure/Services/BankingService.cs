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
        public event EventHandler<TransactionEventHandler>? TransactionProcessed;
        public event EventHandler<AccountEventArgs>? AccountCreated;
        public void CreateCustomer(string firstName, string lastName, string email, string phoneNumber, 
           Address address,DateTime dateOfBirth,  UserPermission permissions = default)
        {
            var customerToCheck = _customerRepository.GetByEmail(email);
            if (customerToCheck != null)
            {
                throw new BankingException( $"the Email{email} is already exist","Duplicated_mail");
            }
            string id= _customerRepository.Count.ToString();
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
        public void CreateAccount(string customerId, AccountType.Type type, decimal initialDeposit = 0, string? accountNumberPrefix = null)
        {
            var AccountToCheck = _accountRepository.GetById(customerId);
            if (AccountToCheck != null && AccountToCheck.AccountType == type)
            {
                throw new BankingException($"you alre","duplicated_Account");
            }
        }
    }
}
