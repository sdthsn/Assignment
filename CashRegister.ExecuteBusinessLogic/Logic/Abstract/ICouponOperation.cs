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

        Task<UIO.Coupon> FindCouponAsync(string promoCode);
        Task AddCouponAsync(UIO.Coupon coupon);
    }
}
