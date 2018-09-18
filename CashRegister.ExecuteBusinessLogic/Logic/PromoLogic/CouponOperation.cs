using CashRegister.Domain.Abstract;
using System;
using UIO = CashRegister.ExecuteBusinessLogic.Model;
using DBO= CashRegister.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.ExecuteBusinessLogic.Logic.Abstract;

namespace CashRegister.ExecuteBusinessLogic.Logic.PromoLogic
{
   public  class CouponOperation : ICouponOperation
    {
        private readonly ICRCouponRepository _cRepo;
        public CouponOperation(ICRCouponRepository cRepo)
        {
            _cRepo = cRepo;
        }

        public async Task<UIO.Coupon> FindCouponAsync(string promoCode)
        {
            try
            {
                DBO.Coupon dbCoupon = await _cRepo.GetCouponAsync(promoCode);
                if (dbCoupon == null) return null;
                return DboCouponToUio(dbCoupon);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task AddCouponAsync(UIO.Coupon coupon)
        {
            try
            {
                var checkdbCoupon = _cRepo.GetCoupon(coupon.PromoCode);
                if (checkdbCoupon != null)
                {
                    throw new ArgumentNullException("coupon");
                }
                await _cRepo.SaveCouponAsync(UioCouponToDbo(coupon));



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        //ToDo: Need to implement rest of business logic as required for now it was not asked

        #region Mapping

        private UIO.Coupon DboCouponToUio(DBO.Coupon coupon)
        {
            return new UIO.Coupon
            {
                PromoCode = coupon.PromoCode,
                PromoName = coupon.PromoName,
                PromoDescription = coupon.PromoDescription,
                IsBxGy = coupon.IsBxGy,
                BuyX = coupon.BuyX,
                GetY = coupon.GetY,
                IsDiscounted = coupon.IsDiscounted,
                MinimumPurchaseRequire= coupon.MinimumPurchaseRequire,
                DiscountPercentage = coupon.DiscountPercentage,
                ProductId = coupon.ProductId,
                ISFullPurchaseDiscount =coupon.ISFullPurchaseDiscount,
                FullPurchaseDiscount=coupon.FullPurchaseDiscount,


            };
        }
        private DBO.Coupon UioCouponToDbo(UIO.Coupon coupon)
        {
            return new DBO.Coupon
            {
                PromoCode = coupon.PromoCode,
                PromoName = coupon.PromoName,
                PromoDescription = coupon.PromoDescription,
                IsBxGy = coupon.IsBxGy,
                BuyX = coupon.BuyX,
                GetY = coupon.GetY,
                IsDiscounted = coupon.IsDiscounted,
                MinimumPurchaseRequire = coupon.MinimumPurchaseRequire,
                DiscountPercentage = coupon.DiscountPercentage,
                ProductId = coupon.ProductId,
                ISFullPurchaseDiscount = coupon.ISFullPurchaseDiscount,
                FullPurchaseDiscount = coupon.FullPurchaseDiscount,

            };
        }
        //ToDo: Need to implement rest of business logic as required for now it was not asked
        #endregion
    }
}
