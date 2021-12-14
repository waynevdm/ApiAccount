using AccountData;
using AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBusiness
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepo;

        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _accountRepo.GetAll();
        }
        public async Task<Customer> Deposit(string idNumber, decimal amount)
        {
            if (amount > 100)
                throw new Exception("Can not deposit more than 100.");

            return await _accountRepo.Deposit(idNumber, amount);
        }

        public async Task<Customer> Withdraw(string idNumber, decimal amount)
        {
            return await _accountRepo.Withdraw(idNumber, amount);
        }
    }
}
