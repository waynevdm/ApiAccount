using AccountBusiness;
using AccountTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AccountTests.Unit
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public async Task AccountServiceDepositTooMuchTest()
        {
            // Arrange
            var acctService = new AccountService(AccountRepoMockHelper.GetMock());

            // Act
            var resp = await acctService.Deposit("545464534663", 500.00M);

            // Assert
            Assert.IsTrue(resp.HasErrors());
            Assert.IsTrue(resp.Errors.Contains("Can not deposit more than 100."));
        }
    }
}
