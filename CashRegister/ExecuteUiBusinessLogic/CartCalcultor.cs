using CashRegister.ExecuteBusinessLogic.Model;
using CashRegister.Model.UIHelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.ExecuteUiBusinessLogic
{
    public class CartCalcultor
    {
        private readonly string _wKey;
        private readonly string _qKey;
        public CartCalcultor()
        {
            _wKey = "w";
            _qKey = "q";
        }
        #region PriceCalculation

        // Directly calculate the total if no promo applied
        public double CartTotal(Dictionary<string, CartInfo> cart)
        {
            return CalculateTotal(cart);
        }

        public double CartTotalWithOverAllDiscount(Dictionary<string, CartInfo> cart, Coupon coupon)
        {
            var total = CalculateTotal(cart);

            return DiscountByPercent(total, coupon.FullPurchaseDiscount);
        }

        // method implements the business logic to apply dicscount using the helper methods in the private section
        public DiscountedCartInfo CartAfterPromo(Dictionary<string, CartInfo> cart, Coupon coupon)
        {
            if (coupon.ISFullPurchaseDiscount)
            {
                var total = CalculateTotal(cart);
                return new DiscountedCartInfo
                {
                    Cart = cart,
                    IsFullDiscount = true,

                };
            }

            if (coupon.IsBxGy || coupon.IsDiscounted)
            {
                if (coupon.IsBxGy)
                {
                    string searchKey = string.Concat(coupon.ProductId, "-", _qKey);
                    if (cart.ContainsKey(searchKey))
                    {
                        var totalCount = cart[searchKey].Count.Total;
                        if (totalCount > coupon.BuyX)
                        {
                            int free = CountFreeAfterDiscount(totalCount, coupon.BuyX, coupon.GetY);

                            cart[searchKey].Count.Free = free;
                        }
                    }
                }
                if (coupon.IsDiscounted)
                {
                    string searchKey = string.Concat(coupon.ProductId, "-", _wKey);
                    if (cart.ContainsKey(searchKey))
                    {
                        if (cart[searchKey].Weight.WeightTotal > coupon.MinimumPurchaseRequire)
                        {
                            double discountedPrice = DiscountByPercent(cart[searchKey].Product.PricePerPound, coupon.DiscountPercentage);
                            cart[searchKey].Weight.DiscountedPrice = discountedPrice;
                        }
                    }
                }
            }
            return new DiscountedCartInfo
            {
                Cart = cart,
                IsFullDiscount = false,

            };
        }


        #endregion

        // Below methods are all for internal use as a pricing and discount calclator


        #region HelperMethods 


        // This method applies the discount on certain price
        private double DiscountByPercent(double price, double discount)
        {
            return price - price * (discount / 100);
        }
        // This method calculate the total price for a given cart
        private double CalculateTotal(Dictionary<string, CartInfo> cart)
        {
            var total = cart.Values.Sum(x => ((x.Count.Total - x.Count.Free) * x.Product.PricePerUnit) + (x.Weight.WeightTotal * x.Weight.DiscountedPrice)); //Weight is multiplied by x.Weight.DiscountedPrice because intially x.Weight.DiscountedPrice is updated as if product price

            return total;
        }


        //This method will be used  for Buy X get Y free Calculation and will update the Cart accordingly
        private int CountFreeAfterDiscount(int count, int buyX, int getYFree)
        {
            int item = count;
            int free = 0;

            while (item >= buyX + getYFree)
            {
                free += getYFree;
                item = item - (buyX + getYFree);
            }

            if (item - buyX > 0) free += (item - buyX);

            return free;
        }

        #endregion
    }

}
