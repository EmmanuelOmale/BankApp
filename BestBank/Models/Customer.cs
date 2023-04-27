
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Customer
    {
        public List<Account> Account = new List<Account>();
        public string AccountName { get; set; }

    }
}
