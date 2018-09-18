using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model.UIHelperModel
{
    public class DiscountedCartInfo
    {
        public Dictionary<string, CartInfo> Cart { get; set; }
        public bool IsFullDiscount { get; set; }

    }
}
