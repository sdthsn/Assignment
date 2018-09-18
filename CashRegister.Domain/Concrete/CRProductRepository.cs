using CashRegister.Domain.Abstract;
using CashRegister.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Concrete
{
    public class CRProductRepository : ICRProductRepository
    {
        private readonly CRDbContext _context;
        private string errorMessage ;

        public CRProductRepository()
        {
           _context = new CRDbContext();
            errorMessage = string.Empty;
        }


        public Product GetProduct(string sku)
        {
            //Product dbProduct = _context.Products.Where(x => x.Sku == sku).FirstOrDefault();
            Product dbProduct = _context.Products.Find(sku);

            return dbProduct;
        }

        public void SaveProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException("product");
                }
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);

                throw new Exception(errorMessage, ex);
            }
        }

        public void UpdateProduct( Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException("product");
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }
        }


        public void  DeleteProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException("product");
                }
                //_context.Products.Remove(product);
                _context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }

        }

        #region Management Helper
        // Returns all the product exist in Data Base, this method wiil be used only for managerial access
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList(); 
        }
        #endregion

        #region Exceptions help method

        // This method is a unique to handle exception during insert, update and delete
        private string DbExceptionHelper(DbEntityValidationException ex)
        {
            string errorDetails=null;
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    errorDetails += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                }
            }
            return errorDetails;
        }
        #endregion
    }
}
