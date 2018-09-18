using CashRegister.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Abstract
{
   public  interface ICRCouponRepository
    {
        Coupon GetCoupon(string promoCode);
        void SaveCoupon(Coupon coupon);
        void UpdateCoupon(Coupon coupon);  
        void DeleteCoupon(Coupon coupon);
        IEnumerable<Coupon> Coupons();
    }
}
