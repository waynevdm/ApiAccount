using AccountData;
using AccountModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTests.Mock
{
    public static class AccountRepoMockHelper
    {
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

        public static IAccountRepository GetMock()
        {
            var accountRepoMock = new Mock<IAccountRepository>();

            accountRepoMock.Setup(p => p.GetAll()).Returns(Task.FromResult(_customers.AsEnumerable<Customer>()));
            accountRepoMock.Setup(p => p.Deposit(It.IsAny<string>(), It.IsAny<decimal>())).Returns<string, decimal>(
                (idNumber, amount) =>
                {
                    var customer = _customers.FirstOrDefault(x => x.IdNumber == idNumber);
                    if (customer is null)
                        throw new Exception($"No customer found for IdNumber: {idNumber}");

                    customer.Accounts.First().Balance += amount;
                    return Task.FromResult(customer);
                }
            );
            accountRepoMock.Setup(p => p.Withdraw(It.IsAny<string>(), It.IsAny<decimal>())).Returns<string, decimal>(
                (idNumber, amount) =>
                {
                    var customer = _customers.FirstOrDefault(x => x.IdNumber == idNumber);
                    if (customer is null)
                        throw new Exception($"No customer found for IdNumber: {idNumber}");

                    customer.Accounts.First().Balance -= amount;
                    return Task.FromResult(customer);
                }
            );

            return accountRepoMock.Object;
        }
    }
}
