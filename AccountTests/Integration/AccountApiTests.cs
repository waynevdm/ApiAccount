using AccountModel;
using AccountModel.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AccountTests.Integration
{
    [TestClass]
    public class AccountApiTests
    {
        [TestMethod]
        public void AccountTests()
        {
            // Get All
            var client = new RestClient("https://localhost:5001/");
            var getAccountReq = new RestRequest("/Customer");
            var accountResponse = client.Get<IEnumerable<Customer>>(getAccountReq);
            Assert.IsNotNull(accountResponse);
            Assert.AreEqual(accountResponse.StatusCode, HttpStatusCode.OK);

            var customer = accountResponse.Data.FirstOrDefault(x => x.IdNumber == "545464534663");
            var originalBalance = customer.Accounts[0].Balance;

            // Depost
            var depositReq = new RestRequest("/Customer/deposit");
            depositReq.RequestFormat = DataFormat.Json;

            var accountUpdate = new AccountUpdateModel
            {
                IdNumber = "545464534663",
                Amount = 50.00M
            };
            depositReq.AddJsonBody(accountUpdate);

            var depositResponse = client.Post<Customer>(depositReq);
            Assert.IsNotNull(depositResponse);
            Assert.AreEqual(depositResponse.StatusCode, HttpStatusCode.OK);

            var balanceAfterDeposit = depositResponse.Data.Accounts[0].Balance;
            Assert.AreEqual(balanceAfterDeposit, originalBalance + 50);

            // Withdraw
            var withdrawReq = new RestRequest("/Customer/withdraw");
            withdrawReq.RequestFormat = DataFormat.Json;

            accountUpdate = new AccountUpdateModel
            {
                IdNumber = "545464534663",
                Amount = 30.00M
            };
            withdrawReq.AddJsonBody(accountUpdate);

            var withdrawResponse = client.Post<Customer>(withdrawReq);
            Assert.IsNotNull(withdrawResponse);
            Assert.AreEqual(withdrawResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(withdrawResponse.Data.Accounts[0].Balance, balanceAfterDeposit - 30);
        }
    }
}
