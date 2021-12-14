using AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBusiness
{
    public interface IAccountService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Deposit(string idNumber, decimal amount);
        Task<Customer> Withdraw(string idNumber, decimal amount);
    }
}
