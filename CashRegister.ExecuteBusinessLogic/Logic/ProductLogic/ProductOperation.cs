using CashRegister.Domain.Concrete;
using CashRegister.Domain.Abstract;
using DBO = CashRegister.Domain.Entities;  //DBO = Database objects
using UIO = CashRegister.ExecuteBusinessLogic.Model; //UIO = User Interface objects
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.ExecuteBusinessLogic.Logic.Abstract;

namespace CashRegister.ExecuteBusinessLogic.Logic.ProductLogic
{
    public class ProductOperation : IProductOperation
    {
        private readonly ICRProductRepository _pRepo;
        public ProductOperation(ICRProductRepository pRepo)
        {
            _pRepo = pRepo;
        }

        public async Task<UIO.Product> FindProductAsync(string sku)
        {
            try
            {
                DBO.Product dbProduct =await _pRepo.GetProductAsync(sku);
                if (dbProduct == null) return null;
                return DboProductToUio(dbProduct);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task AddProductAsync(UIO.Product product)
        {
            try
            {
                var checkproduct = _pRepo.GetProduct(product.Sku);
                if(checkproduct != null)
                {
                    throw new ArgumentNullException("product");
                }
                await _pRepo.SaveProductAsync(UioProductToDbo(product));
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task EditProductAsync(UIO.Product product)
        {
            try
            {
                var checkproduct = _pRepo.GetProduct(product.Sku);
                if (checkproduct != null)
                {
                    throw new ArgumentNullException("product");
                }

                await _pRepo.UpdateProductAsync(UioProductToDbo(product));           
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task DeleteProductAsync(string sku)
        {
            try
            {
                var checkproduct = _pRepo.GetProduct(sku);
                if (checkproduct == null)
                {
                    throw new ArgumentNullException("product");
                }
                await  _pRepo.DeleteProductAsync(checkproduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        #region Mapping 
        //UI model to Data Model and vice-versa
        /* In real peoject I will definitely use Automapper nuget pachage instead of this back dated manual conversion*/

        public UIO.Product DboProductToUio(DBO.Product product)
        {
            return new UIO.Product
            {
                Sku = product.Sku,
                ProductName = product.ProductName,
                ProductShortName = product.ProductShortName,
                ProductDescription = product.ProductDescription,
                PricePerUnit = product.PricePerUnit,
                IsSoldByQuantity = product.IsSoldByQuantity,
                IsSoldByWeight = product.IsSoldByWeight,
                PricePerPound = product.PricePerPound
            };
        }
        public DBO.Product UioProductToDbo(UIO.Product product)
        {
            return new DBO.Product
            {
                Sku = product.Sku,
                ProductName = product.ProductName,
                ProductShortName = product.ProductShortName,
                ProductDescription = product.ProductDescription,
                PricePerUnit = product.PricePerUnit,
                IsSoldByQuantity = product.IsSoldByQuantity,
                IsSoldByWeight = product.IsSoldByWeight,
                PricePerPound = product.PricePerPound
            };
        }

        #endregion
    }
}
