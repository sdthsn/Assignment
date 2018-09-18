using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Entities
{
   public class Coupon
    {
        /* In the real life I will never use this type Model validation 
         Instead I will use Fluent Validator Nuget Packages to validate models.
         I did not use Fluent validator because it is time consuming    
        */

        [Key]
        public string PromoCode { get; set; }
        [Required]
        public string PromoName { get; set; }
        [Required]
        public string PromoDescription { get; set; }
        [Required]
        public bool IsBxGy { get; set; }
        [Required]
        public int BuyX { get; set; }
        [Required]
        public int GetY { get; set; }
        [Required]
        public bool IsDiscounted { get; set; }
        [Required]
        public double MinimumPurchaseRequire { get; set; }
        [Required]
        public double DiscountPercentage { get; set; }
        [Required]
        public bool ISFullPurchaseDiscount { get; set; }
        [Required]
        public double FullPurchaseDiscount { get; set; }
        [Required]
        public string ProductId { get; set; }
    }
}
