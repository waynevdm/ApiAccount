using AccountModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return new List<Customer>()
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
        }
    }
}
