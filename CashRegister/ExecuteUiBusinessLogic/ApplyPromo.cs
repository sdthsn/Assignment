using CashRegister.ExecuteBusinessLogic.Logic.Abstract;
using CashRegister.ExecuteBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.ExecuteUiBusinessLogic
{
    public class ApplyPromo
    {
        private readonly ICouponOperation _cOp;

        public ApplyPromo(ICouponOperation cOp)
        {
            _cOp = cOp;
        }

        public async Task<Coupon> AddCoupon()
        {
            Console.Write(" Do you have a coupon? Type 'y' for Yes else type any key :");
            string answer = Console.ReadLine();

            if (answer.Equals("y"))
            {
                Console.Write(" Scane your Coupon :");
                bool inValid = true;
                while (inValid)
                {
                    string code = Console.ReadLine();
                    if (!string.IsNullOrEmpty(code) && !string.IsNullOrWhiteSpace(code))
                    {
                        inValid = false;
                        var coupon =await GetCoupon(code);
                        if (coupon != null)
                        {
                            return coupon;
                        }
                        else
                        {
                            Console.WriteLine("Invalid coupon");
                        }
                    }
                    else
                    {
                        Console.Write("Please enter number or calender :");
                    }

                }
            }
            return null;
        }

        //This method is NotImplementedException to handle multiple coupon, though we did not implement UI for this at this time
        private  IList<Coupon> ScanCoupons()
        {
            IList<Coupon> coupons = new List<Coupon>();

            Console.Write(" Do you have a coupon? Type 'y' for Yes else type any key :");
            string answer = Console.ReadLine();

            if(answer.Equals("y"))
            {
                bool disCont = true;
                while (disCont)
                {
                    var coupon = AddCoupon().Result;
                    if (coupon != null)
                    {
                        coupons.Add(coupon);
                    }
                    Console.Write(" Do you have more coupon? Type 'y' for Yes else type any key :");
                    string more = Console.ReadLine();
                    if (!answer.Equals("y"))
                    {
                        disCont = false;
                    }
                                       
                }
            }
            return coupons;
        }

       

        private async Task<Coupon> GetCoupon(string prompCode)
        {
            return await _cOp.FindCouponAsync(prompCode);
        }
    }
}
