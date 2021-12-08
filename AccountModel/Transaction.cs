using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModel
{
    public enum TransactionType
    {
        Debit,
        Credit,
    }
    public class Transaction
    {
        public DateTime UpdatedOn { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
