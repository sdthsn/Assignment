using CashRegister.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Concrete
{
    public class CRDbContext :DbContext 
    {
        public CRDbContext() : base()
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

    }
}
