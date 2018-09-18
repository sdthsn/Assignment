using CashRegister.InitialiseDB;
using CashRegister.ExecuteUiBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    class Program
    {
        static void Main(string [] args)
        {
            /*Uncomment below 2 lines if you are running this app for the first time 
             Once your data is created comment it again
             */
            //TODO:
            var prepareDbData = new Data();
            prepareDbData.InitializeDB();

            var cRegister = new CashRegisterer();
            cRegister.CashRegisterOperation();

            Console.ReadKey();
        }
    }
}
