using CashRegister.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Abstract
{
    public interface ICRProductRepository
    {
        Task<Product> GetProductAsync(string sku);
        Task SaveProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        

        Product GetProduct(string sku);
        void SaveProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);


        IEnumerable<Product> GetAllProducts();
        Task<IEnumerable<Product>> GetAllProductsAsync();

    }
}
