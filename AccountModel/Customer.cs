using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModel
{
    public class Customer
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public IList<Account> Accounts { get; set; } = new List<Account>();
    }
}
