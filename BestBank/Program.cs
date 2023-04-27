
using BankApp.Core;
using BankApp.Utilities;

namespace BankApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GlobalConfig globalConfig = new GlobalConfig();
            DataManager dataManager = new DataManager(globalConfig);

            do
            {
                DataManager.Notify("Hello; You are welocme to MyBank", true);
                dataManager.Processor(DataManager.GeneralMenu());
            } while (true);

        }
    }
}










