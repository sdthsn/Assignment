using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Entities.Validation
{
    class CouponValidation : EntityTypeConfiguration<Coupon>
    {
        public CouponValidation()
        {
            HasKey(x => x.PromoCode);
            Property(x => x.PromoCode).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PromoName).IsRequired();
            Property(x => x.IsBxGy).IsRequired();
            Property(x => x.IsDiscounted).IsRequired();
            Property(x => x.ISFullPurchaseDiscount).IsRequired();
            Property(x => x.ProductId).IsRequired();

            ToTable("Coupon");
        }
    
    }
}
