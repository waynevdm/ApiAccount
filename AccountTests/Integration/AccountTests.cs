using AccountModel;
using AccountModel.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AccountTests.Integration
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void AccountApiTests()
        {
            // Get All
            var client = new RestClient("https://localhost:44361/");
            var getAccountReq = new RestRequest("/Customer");
            var accountResponse = client.Get<IEnumerable<Customer>>(getAccountReq);
            Assert.IsNotNull(accountResponse);
            Assert.AreEqual(accountResponse.StatusCode, HttpStatusCode.OK);

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
        }
    }
}
