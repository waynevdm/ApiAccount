using AccountModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace AccountTests.Integration
{
    [TestClass]
    public class AccountDBTest
    {
        [TestMethod]
        public void InsertCustomersXML()
        {
            List<Customer> customers = new List<Customer>()
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
            string xml = JsonConvert.SerializeObject(customers, Formatting.Indented);

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=AccountDatabase;Integrated Security=True";
                connection.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO Customer VALUES(1, '{xml}')", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
