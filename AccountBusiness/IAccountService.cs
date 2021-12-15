using AccountBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBusiness
{
    public interface IAccountService
    {
        Task<BusinessResponse> GetAll();
        Task<BusinessResponse> Deposit(string idNumber, decimal amount);
        Task<BusinessResponse> Withdraw(string idNumber, decimal amount);
    }
}
