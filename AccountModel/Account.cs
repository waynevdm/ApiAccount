using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModel
{
    public enum AccountType
    {
        Cheque,
        Savings,
        Credit,
    }
    public class Account
    {
        public AccountType Type { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal Balance { get; set; }

        public IList<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
    }
}
