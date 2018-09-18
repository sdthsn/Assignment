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
        Task<UIO.Product> FindProductAsync(string sku);
        Task AddProductAsync(UIO.Product product);
        Task EditProductAsync(UIO.Product product);
        Task DeleteProductAsync(string sku);
    }
}
