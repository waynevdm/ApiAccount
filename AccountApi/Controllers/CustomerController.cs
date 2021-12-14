using AccountApi.Models;
using AccountBusiness;
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
        private readonly ILogger<CustomerController> _logger;
        private readonly IAccountService _accountService;

        public CustomerController(ILogger<CustomerController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _accountService.GetAll();
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<Customer> Deposit(AccountUpdateModel model)
        {
            return await _accountService.Deposit(model.IdNumber, model.Amount);
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<Customer> Withdraw(AccountUpdateModel model)
        {
            return await _accountService.Withdraw(model.IdNumber, model.Amount);
        }
    }
}
