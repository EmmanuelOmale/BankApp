using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Helpers
{
    public class Menu
    {
        public static List<string> GeneralMenu = new List<string>()
        {
            "Create Account",
            "Exit"
        };
        public static List<string> AccountMenu = new List<string>()
        {

            "Deposit",
            "Withdraw",
            "Transfer",
            "Check Balance",
            "Check Account Details",
            "Print Statement of Account",
            "Switch Account",
            "Add Another Account",
            "Exit"
        };

    }
}
