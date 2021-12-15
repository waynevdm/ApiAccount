using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountModel.Api
{
    public class AccountUpdateModel
    {
        public string IdNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
