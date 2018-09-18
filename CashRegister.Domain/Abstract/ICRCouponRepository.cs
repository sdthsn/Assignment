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
        Task<Coupon> GetCouponAsync(string promoCode);
        Task SaveCouponAsync(Coupon coupon);
        Task UpdateCouponAsync(Coupon coupon);
        Task DeleteCouponAsync(Coupon coupon);
        Task<IEnumerable<Coupon>> GetCouponsAsync();

        Coupon GetCoupon(string promoCode);
        void SaveCoupon(Coupon coupon);
        void UpdateCoupon(Coupon coupon);  
        void DeleteCoupon(Coupon coupon);
        IEnumerable<Coupon> GetCoupons();
    }
}
