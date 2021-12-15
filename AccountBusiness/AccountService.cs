using AccountBusiness.Model;
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

        public async Task<BusinessResponse> GetAll()
        {
            return new BusinessResponse
            {
                Data = await _accountRepo.GetAll()
            };
        }
        public async Task<BusinessResponse> Deposit(string idNumber, decimal amount)
        {
            if (amount > 100)
            {
                var resp = new BusinessResponse();
                resp.Errors.Add("Can not deposit more than 100.");
                return resp;
            }

            return new BusinessResponse
            {
                Data = await _accountRepo.Deposit(idNumber, amount)
            };
        }

        public async Task<BusinessResponse> Withdraw(string idNumber, decimal amount)
        {
            return new BusinessResponse
            {
                Data = await _accountRepo.Withdraw(idNumber, amount)
            }; 
        }
    }
}
