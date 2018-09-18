using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIO = CashRegister.ExecuteBusinessLogic.Model;

namespace CashRegister.ExecuteBusinessLogic.Logic.Abstract
{
    public interface ICouponOperation
    {

        UIO.Coupon FindCoupon(string promoCode);
        void Addcoupon(UIO.Coupon coupon);
    }
}
