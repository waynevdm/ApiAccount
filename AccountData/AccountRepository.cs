using AccountModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccountData
{
    public class AccountRepository : IAccountRepository
    {
        private IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var connectionString = _configuration.GetConnectionString("Customer");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string commandText = "SELECT Data FROM Customer WHERE Id = 1";

                SqlCommand cmd = new SqlCommand(commandText, connection);
                using (SqlDataReader sqlReader = await cmd.ExecuteReaderAsync())
                {

                    sqlReader.Read();
                    return JsonConvert.DeserializeObject<IList<Customer>>(sqlReader.GetString(0));
                }
            }
        }

        public async Task<Customer> Deposit(string idNumber, decimal amount)
        {
            var customers = await GetAll();
            var customer = customers.FirstOrDefault(x => x.IdNumber == idNumber);
            if (customer is null)
                throw new Exception($"No customer found for IdNumber: {idNumber}");

            customer.Accounts.First().Balance += amount;
            await UpdateCustomers(customers);
            return customer;
        }

        public async Task<Customer> Withdraw(string idNumber, decimal amount)
        {
            var customers = await GetAll();
            var customer = customers.FirstOrDefault(x => x.IdNumber == idNumber);
            if (customer is null)
                throw new Exception($"No customer found for IdNumber: {idNumber}");

            customer.Accounts.First().Balance -= amount;
            await UpdateCustomers(customers);
            return customer;
        }

        private async Task UpdateCustomers(IEnumerable<Customer> customers)
        {
            var xml = JsonConvert.SerializeObject(customers);
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("Customer");
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE Customer SET Data = '{xml}' WHERE Id = 1", connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
