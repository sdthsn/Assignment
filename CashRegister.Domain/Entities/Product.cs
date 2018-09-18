using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Entities
{
    public class Product
    {
        /* In the real life I will never use this type Model validation 
         Instead I will use Fluent Validator Nuget Packages to validate models.
         I did not use Fluent validator because it is time consuming    
        */

        [Key]
        public string Sku { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductShortName { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public bool IsSoldByQuantity { get; set; }
        [Required]
        public double PricePerUnit { get; set; }
        [Required]
        public bool IsSoldByWeight { get; set; }
        [Required]
        public double PricePerPound { get; set; }
    }
}
