using CashRegister.ExecuteBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model.UIHelperModel
{
    public class CartInfo
    {
        public Product Product { get; set; }
        public Count Count { get; set; }
        public Weight Weight { get; set; }
    }

    public class Count
    {
        public int Total { get; set; }
        public int Free { get; set; }

    }
    public class Weight
    {
        public double WeightTotal { get; set; }
        public double DiscountedPrice { get; set; }
    }
}
