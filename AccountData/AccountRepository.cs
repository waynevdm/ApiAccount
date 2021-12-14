using AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountData
{
    public class AccountRepository : IAccountRepository
    {
        // Fake DB code...
        private static List<Customer> _customers = new List<Customer>()
        {
            new Customer
            {
                Title = "Mr",
                FirstName = "Wayne",
                MiddleName = "Mark",
                Surname = "van der Merwe",
                IdNumber = "545464534663",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        Type = AccountType.Cheque,
                        Balance = 100.00M,
                        LastUpdate = new DateTime(2021, 8, 12, 12, 37, 44),
                        TransactionHistory = new List<Transaction>
                        {
                            new Transaction
                            {
                                UpdatedOn = new DateTime(2021, 7, 10, 10, 12, 15),
                                Type = TransactionType.Credit,
                                Amount = 50.0M
                            },
                            new Transaction
                            {
                                UpdatedOn = new DateTime(2021, 8, 12, 12, 37, 44),
                                Type = TransactionType.Credit,
                                Amount = 50.0M
                            },
                        }
                    }
                }
            },
            new Customer
            {
                Title = "Mr",
                FirstName = "Mark",
                MiddleName = "",
                Surname = "Davies",
                IdNumber = "5454645346783",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        Type = AccountType.Cheque,
                        Balance = 50.00M,
                        LastUpdate = new DateTime(2020, 8, 12, 12, 37, 44),
                        TransactionHistory = new List<Transaction>
                        {
                            new Transaction
                            {
                                UpdatedOn = new DateTime(2020, 7, 10, 10, 12, 15),
                                Type = TransactionType.Credit,
                                Amount = 25.0M
                            },
                            new Transaction
                            {
                                UpdatedOn = new DateTime(2020, 8, 12, 12, 37, 44),
                                Type = TransactionType.Credit,
                                Amount = 25.0M
                            },
                        }
                    }
                }
            }
        };

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Task.FromResult(_customers);
        }

        public async Task<Customer> Deposit(string idNumber, decimal amount)
        {
            var customer = _customers.FirstOrDefault(x => x.IdNumber == idNumber);
            if (customer is null)
                throw new Exception($"No customer found for IdNumber: {idNumber}");

            customer.Accounts.First().Balance += amount;
            return await Task.FromResult(customer);
        }

        public async Task<Customer> Withdraw(string idNumber, decimal amount)
        {
            var customer = _customers.FirstOrDefault(x => x.IdNumber == idNumber);
            if (customer is null)
                throw new Exception($"No customer found for IdNumber: {idNumber}");

            customer.Accounts.First().Balance -= amount;
            return await Task.FromResult(customer);
        }
    }
}
