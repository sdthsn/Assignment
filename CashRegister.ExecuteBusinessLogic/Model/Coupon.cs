using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.ExecuteBusinessLogic.Model
{
    public class Coupon
    {
        public string PromoCode { get; set; }
        public string PromoName { get; set; }
        public string PromoDescription { get; set; }
        public bool IsBxGy { get; set; }
        public int BuyX { get; set; }
        public int GetY { get; set; }
        public bool IsDiscounted { get; set; }
        public double MinimumPurchaseRequire { get; set; }
        public double DiscountPercentage { get; set; }
        public bool ISFullPurchaseDiscount { get; set; }
        public double FullPurchaseDiscount { get; set; }
        public string ProductId { get; set; }

    }
}
