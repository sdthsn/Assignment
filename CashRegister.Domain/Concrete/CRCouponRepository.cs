using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Abstract;
using CashRegister.Domain.Entities;

namespace CashRegister.Domain.Concrete
{
    public class CRCouponRepository : ICRCouponRepository
    {
        private readonly CRDbContext _context;
        private string errorMessage;

        public CRCouponRepository()
        {
            _context = new CRDbContext();
            errorMessage = string.Empty;
        }

        public async Task<Coupon> GetCouponAsync(string promoCode)
        {
            Coupon dbCoupon = await _context.Coupons.FirstOrDefaultAsync(x=>x.PromoCode==promoCode);

            return dbCoupon;
        }

        public async Task SaveCouponAsync(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                _context.Coupons.Add(coupon);
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);

                throw new Exception(errorMessage, ex);
            }

        }

        public async Task  UpdateCouponAsync(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                _context.Coupons.Attach(coupon);
                _context.Entry(coupon).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task DeleteCouponAsync(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                _context.Coupons.Attach(coupon);
                _context.Entry(coupon).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }

        }

        #region Sync Methods if needed

        public Coupon GetCoupon(string promoCode)
        {
            //Coupon dbCoupon = _context.Coupon.Where(x => x.PromoCode == promoCode).FirstOrDefault();
            Coupon dbCoupon = _context.Coupons.Find(promoCode);

            return dbCoupon;
        }

        public void SaveCoupon(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);

                throw new Exception(errorMessage, ex);
            }
        
        }

        public void UpdateCoupon(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }
        }

        public void  DeleteCoupon(Coupon coupon)
        {
            try
            {
                if (coupon == null)
                {
                    throw new ArgumentNullException("coupon");
                }
                //_context.Coupons.Remove(coupon);
                _context.Entry(coupon).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = DbExceptionHelper(ex);
                throw new Exception(errorMessage, ex);
            }

        }
        #endregion

        #region Management Helper

        // Returns all the coupons exist in Data Base, this method will be used only for managerial access
        public IEnumerable<Coupon> GetCoupons()
        {
            return _context.Coupons.ToList();
        }

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
        }
        #endregion
        #region Exceptions help method

        // This method is a unique to handle exception during insert, update and delete
        private string DbExceptionHelper(DbEntityValidationException ex)
        {
            string errorDetails = null;
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
