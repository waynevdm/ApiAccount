using AccountData;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTests.Integration
{
    [TestClass]
    public class AccountRepositoryTest
    {
        [TestMethod]
        public async Task AccountRepoTests()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "Customer")]).Returns("Data Source=(localdb)\\ProjectsV13;Initial Catalog=AccountDatabase;Integrated Security=True");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            var repo = new AccountRepository(mockConfiguration.Object);

            // Get All
            var customers = await repo.GetAll();
            Assert.IsTrue(customers.Count() == 2);

            var customer = customers.FirstOrDefault(x => x.IdNumber == "545464534663");
            var originalBalance = customer.Accounts[0].Balance;

            // Deposit
            customer = await repo.Deposit("545464534663", 50);
            Assert.AreEqual(customer.Accounts[0].Balance, originalBalance + 50);

            var balanceAfterDeposit = customer.Accounts[0].Balance;

            // Withdraw
            customer = await repo.Withdraw("545464534663", 20);
            Assert.AreEqual(customer.Accounts[0].Balance, balanceAfterDeposit - 20);
        }
    }
}
