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
            /*Uncomment the 2 lines in "Database Initialization" region if you are running this app for the first time 
             Once your data is created comment it again
             */
            #region Database Initialization
            ////mAKING A CALL TO INTITIALISE DATABASE WITH TEST DATA
            //TODO:UNCOMMENT
            /*
             var prepareDbData = new Data();     
            prepareDbData.InitializeDB().ConfigureAwait(true);
            */
            #endregion

            var cRegister = new CashRegisterer();
            cRegister.CashRegisterOperation().ConfigureAwait(true);

            Console.ReadKey();
        }
    }
}
