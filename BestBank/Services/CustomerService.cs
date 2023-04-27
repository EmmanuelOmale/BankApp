
using BankApp.Models;
using BankApp.Models.DTOs;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.Services
{
    public class CustomerService : ICustomerService
    {
        public void AddAnotherAccount(AccountDTO accountDto)
        {

            Account newAccount = new Account()
            {
                AccountType = accountDto.AccountType,
                AccountNumber = GenerateAccountNumber(),
                AccountBalance = accountDto.Balance,
                //IsActive = true,
                MinimumBalance = (decimal)accountDto.AccountType
            };
            Transaction transactions = new Transaction()
            {
                Description = "Opened Account",
                Amount = accountDto.Balance,
                Balance = accountDto.Balance,
                Date = DateTime.Now
            };
            newAccount.AccountStatement.Add(transactions);
            var allCustomer = GetAllCustomer();
            foreach (var customer in allCustomer)
            {
                customer.Account.Add(newAccount);
            }
        }
        public decimal CheckBalance(Account account)
        {
            //if (ValidateAccount(account)) 
            return account.AccountBalance;
            //return 0;
        }
        public bool Withdraw(Account account, decimal amount)
        {
            bool success = false;
            if (ValidateAccount(account))
            {
                //if(account.IsActive)
                //{
                if (account.AccountType == AccountType.Savings)
                {
                    if (account.AccountBalance - amount >= 1000)
                    {
                        account.AccountBalance -= amount;
                        success = true;
                    }
                }
                if (account.AccountType == AccountType.Current)
                {
                    if (account.AccountBalance - amount >= 0)
                    {
                        account.AccountBalance -= amount;
                        success = true;
                    }
                }
            }
            if (success)
            {
                Transaction transactions = new Transaction()
                {
                    Description = "Withdrawal",
                    Amount = amount,
                    Balance = account.AccountBalance,
                    Date = DateTime.Now
                };
                account.AccountStatement.Add(transactions);
            }
            //}

            return success;
        }
        public bool Transfer(Account from, Account to, decimal amount)
        {
            bool success = false;
            if (from.AccountBalance - amount >= from.MinimumBalance)
            {
                from.AccountBalance -= amount;
                to.AccountBalance += amount;
                success = true;
            }
            if (success)
            {
                Transaction debiTransactions = new Transaction()
                {
                    Description = $"Tranfer to {to.AccountNumber}",
                    Amount = amount,
                    Balance = from.AccountBalance,
                    Date = DateTime.Now
                };
                from.AccountStatement.Add(debiTransactions);
                Transaction creditTransactions = new Transaction()
                {
                    Description = $"Tranfer received from {to.AccountNumber}",
                    Amount = amount,
                    Balance = to.AccountBalance,
                    Date = DateTime.Now
                };
                to.AccountStatement.Add(creditTransactions);
            }
            return success;
        }
        public Customer CreateAccount(CustomerDTO newCustomer)
        {
            Account account = new Account()
            {
                AccountType = newCustomer.AccountType,
                AccountNumber = GenerateAccountNumber(),
                AccountBalance = newCustomer.Balance,
                //IsActive = true,
                MinimumBalance = (decimal)newCustomer.AccountType
            };
            Transaction transactions = new Transaction()
            {
                Description = "Opened Account",
                Amount = account.AccountBalance,
                Balance = account.AccountBalance,
                Date = DateTime.Now
            };
            account.AccountStatement.Add(transactions);
            Customer customer = new Customer()
            {
                AccountName = newCustomer.Name
            };
            customer.Account.Add(account);
            return customer;
        }
        public bool Deposit(Account account, decimal amount)
        {
            if (ValidateAccount(account))
            {
                account.AccountBalance += amount;
                Transaction creditTransactions = new Transaction()
                {
                    Description = "Deposit",
                    Amount = amount,
                    Balance = account.AccountBalance,
                    Date = DateTime.Now
                };
                account.AccountStatement.Add(creditTransactions);
                return true;
            }
            return false;
        }
        static string GenerateAccountNumber()
        {
            List<string> allAccounts = GetAllCustomerAccount();
            bool reload = false;
            string accountNumber;
            do
            {
                Random random = new Random();
                accountNumber = random.Next(0000000001, 123456789).ToString();
                if (accountNumber.Length != 10) reload = true;
                else
                {
                    if (!allAccounts.Contains(accountNumber)) reload = false;
                }
            }
            while (reload);
            return accountNumber;
        }
        static List<Customer> GetAllCustomer()
        {
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in BankApp.Store.DbStore.Customers)
            {
                customerList.Add(customer);
            }
            return customerList;
        }
        static List<string> GetAllCustomerAccount()
        {
            List<string> accountList = new List<string>();
            var allCustomer = GetAllCustomer();
            foreach (var customer in allCustomer)
            {
                foreach (var acc in customer.Account)
                {
                    accountList.Add(acc.AccountNumber);
                }
            }
            return accountList;
        }
        static bool ValidateAccount(Account account)
        {
            for (int i = 0; i < BankApp.Store.DbStore.Customers.Count; i++)
            {
                var dbAccount = BankApp.Store.DbStore.Customers[i].Account;
                foreach (var acc in dbAccount)
                {
                    //if (acc.IsActive)
                    //{
                    if (account == acc) return true;
                    //}
                }
            }
            return false;
        }

        public object Deposit(List<Account> account, int depositAmount)
        {
            throw new NotImplementedException();
        }
    }
}
