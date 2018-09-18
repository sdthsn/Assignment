using CashRegister.Domain.Abstract;
using CashRegister.Domain.Concrete;
using CashRegister.ExecuteBusinessLogic.Logic.Abstract;
using CashRegister.ExecuteBusinessLogic.Logic.ProductLogic;
using CashRegister.ExecuteBusinessLogic.Logic.PromoLogic;
using CashRegister.Model.UIHelperModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashRegister.ExecuteUiBusinessLogic
{
    public class CashRegisterer
    {
        //Database layer, I will never bring this in UI in real project, instead I will use Autofac or Ninject IOC container
        // I could configure the ICRProductRepository and ICRCouponRepository directly in ExecuteBusinessLogic layer, if I'd do so it would violate loosely coupled convention.
        private readonly ICRProductRepository _pR;
        private readonly ICRCouponRepository _cR;
        private readonly IProductOperation _pOp;
        private readonly ICouponOperation _cOp;

        private readonly CartEditor cart;
        private readonly CartCalcultor calculator;
        private readonly ApplyPromo promo;

        public CashRegisterer()
        {
            _pR = new CRProductRepository();
            _cR = new CRCouponRepository();
            _cOp = new CouponOperation(_cR);
            _pOp = new ProductOperation(_pR);
            cart = new CartEditor(_pOp);
            calculator = new CartCalcultor();
            promo = new ApplyPromo(_cOp);

        }
        public async Task CashRegisterOperation()
        {
            var cartTotal = 0.0;
            var cartTotalAfterDiscount = 0.0;
            bool discountOnFull = false;


            /*** Adding product to cart ***/

            var ItemAddToCart = cart.Cart();
            cartTotal = calculator.CartTotal(ItemAddToCart);

            /** Coupon operation begins **/

            var promoInfo =await promo.AddCoupon();

            if (promoInfo != null)
            {
                var discountedCart = calculator.CartAfterPromo(ItemAddToCart, promoInfo);
                if (discountedCart.IsFullDiscount)
                {
                    cartTotalAfterDiscount = calculator.CartTotalWithOverAllDiscount(discountedCart.Cart, promoInfo);
                    discountOnFull = true;
                }
                else
                {
                    cartTotalAfterDiscount = calculator.CartTotal(discountedCart.Cart);

                }

                // Print receipt
                PrintReceipt(discountedCart.Cart, cartTotal, cartTotalAfterDiscount, discountOnFull);

            }
            else
            {
                // Print receipt
                PrintReceipt(ItemAddToCart, cartTotal, cartTotalAfterDiscount, discountOnFull);
            }
        }

        private void PrintReceipt(Dictionary<string, CartInfo> cart, double total, double dTotal, bool discountOnFull)
        {
            Console.Clear();
            Console.WriteLine("/*************************Invoice**********************/");
            Console.WriteLine("                      {0}                  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();
            Console.WriteLine("Sku      Product     Q/W     Unite Price     DiscPrc/unit        Total ");
            foreach (var item in cart)
            {
                if (item.Value.Count.Total > 0)
                {
                    Console.WriteLine("{0}      {1}     {2}      {3}                {4}               {5}",
                        item.Value.Product.Sku,
                        item.Value.Product.ProductName,
                        item.Value.Count.Total,
                        item.Value.Product.PricePerUnit,
                        item.Value.Count.Free,
                        item.Value.Count.Total * item.Value.Product.PricePerUnit);
                }
                if (item.Value.Weight.WeightTotal > 0)
                {
                    Console.WriteLine("{0}      {1}     {2}    {3}/lb              {4}             {5}",
                        item.Value.Product.Sku,
                        item.Value.Product.ProductName,
                        item.Value.Weight.WeightTotal,
                        item.Value.Product.PricePerPound,
                        item.Value.Weight.DiscountedPrice,
                        item.Value.Weight.WeightTotal * item.Value.Weight.DiscountedPrice);
                }
            }
            Console.WriteLine();
            if (dTotal > 0)
            {
                var discountAmount = total - dTotal;
                Console.WriteLine("You saved: ${0}", discountAmount);
                Console.WriteLine("Your total: ${0}", total - discountAmount);
            }
            else
            {
                Console.WriteLine("Your total: ${0}", total);
            }
            if(discountOnFull) Console.WriteLine("XCLUSIVE discount applied on full purchase");

        }


    }
}

