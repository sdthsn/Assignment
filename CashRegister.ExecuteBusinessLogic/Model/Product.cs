using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.ExecuteBusinessLogic.Model
{
    public class Product
    {
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string ProductShortName { get; set; }
        public string ProductDescription { get; set; }
        public bool IsSoldByQuantity { get; set; }
        public double PricePerUnit { get; set; }
        public bool IsSoldByWeight { get; set; }
        public double PricePerPound { get; set; }
    }
}
