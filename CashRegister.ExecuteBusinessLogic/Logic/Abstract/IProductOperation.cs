using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIO = CashRegister.ExecuteBusinessLogic.Model; //UIO = User Interface objects

namespace CashRegister.ExecuteBusinessLogic.Logic.Abstract
{
    public interface IProductOperation
    {
        UIO.Product FindProduct(string sku);
        void AddProduct(UIO.Product product);
        void EditProduct(UIO.Product product);
        void DeleteProduct(string sku);
    }
}
