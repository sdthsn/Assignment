using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Entities.Validation
{
    class ProductValidation : EntityTypeConfiguration<Product>
    {
        public ProductValidation()
        {
            HasKey(x => x.Sku);
            Property(x => x.Sku).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProductName).IsRequired();
            Property(x => x.ProductShortName).IsRequired();
            Property(x => x.IsSoldByQuantity).IsRequired();
            Property(x => x.IsSoldByWeight).IsRequired();

            ToTable("Product");
        }
    }
}
