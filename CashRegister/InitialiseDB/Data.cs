using CashRegister.Domain.Abstract;
using CashRegister.Domain.Concrete;
using CashRegister.ExecuteBusinessLogic.Logic.ProductLogic;
using CashRegister.ExecuteBusinessLogic.Logic.PromoLogic;
using CashRegister.ExecuteBusinessLogic.Model;
using CashRegister.ExecuteBusinessLogic.Logic.Abstract;
using System;
using System.Collections.Generic;

namespace CashRegister.InitialiseDB
{
    public class Data
    {
        private readonly ICRProductRepository _pR;
        private readonly ICRCouponRepository _cR;
        private readonly IProductOperation _pOp;
        private readonly ICouponOperation _cOp;

        public Data()
        {
            _pR = new CRProductRepository();
            _cR = new CRCouponRepository();
            _cOp = new CouponOperation(_cR);
            _pOp = new ProductOperation(_pR);

        }

        public void InitializeDB()
        {
        var products = PrepareProducts();
        var promos = PrepareCoupons();

        //Insering products to DB
        
        foreach(var item in products)
            {
                _pOp.AddProduct(item);
            }
            Console.WriteLine("Products saved, now fetching saved products.....");
            Console.WriteLine();
            foreach (var item in products)
            {
                var p = _pOp.FindProduct(item.Sku);

                Console.WriteLine(p.ProductName);
            }

            Console.WriteLine();
            Console.WriteLine();
            foreach (var item in promos)
            {
                _cOp.Addcoupon(item);
            }

            Console.WriteLine("Coupons saved, now fetching saved coupons.....");
            Console.WriteLine();

            foreach (var item in promos)
            {
                var c = _cOp.FindCoupon(item.PromoCode);

                Console.WriteLine(c.PromoName);
            }

        }

       private IEnumerable<Product> PrepareProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Sku ="111111",ProductName="Apple",ProductShortName="Apple",ProductDescription="Some description",PricePerUnit= 2.30,IsSoldByQuantity=true,IsSoldByWeight=true,PricePerPound= 12.30
                },
                new Product
                {
                    Sku ="111112",ProductName="Orange",ProductShortName="Orange",ProductDescription="Some description of Orange",PricePerUnit= 1.93,IsSoldByQuantity=true,IsSoldByWeight=true,PricePerPound= 8.8
                },
                new Product
                {
                    Sku ="111113",ProductName="Bread",ProductShortName="Bread",ProductDescription="Some description of Bread",PricePerUnit= 2.30,IsSoldByQuantity=true,IsSoldByWeight=false
                },
                new Product
                {
                    Sku ="111114",ProductName="Cococola Can",ProductShortName="Can Coke",ProductDescription="Some description of Coke",PricePerUnit= 1,IsSoldByQuantity=true,IsSoldByWeight=false
                },
                new Product
                {
                    Sku ="111115",ProductName="Spinnach",ProductShortName="Spinnach",ProductDescription="Some description of Spinnach",PricePerPound= 2.30,IsSoldByQuantity=false,IsSoldByWeight=true
                },

                new Product
                {
                    Sku ="000000",ProductName="Miscellaneous",ProductShortName="Misc",ProductDescription="UnRecorded products , cashier need to add price and quantity",IsSoldByQuantity=true,IsSoldByWeight=true
                }
            };

            return products;
        }
       private IEnumerable<Coupon> PrepareCoupons()
        {
            var promos = new List<Coupon>
            {
                new Coupon
                {
                    PromoCode ="AAAA10", PromoName="10 % off",PromoDescription="10 % discount applicable of apple pric eif buy in weight",IsBxGy=false,BuyX=0,GetY=0,IsDiscounted=true,DiscountPercentage=10, ProductId="111111"
                },
                 new Coupon
                {
                    PromoCode ="BBBB14", PromoName="15 % off",PromoDescription="15 % discount applicable of apple pric eif buy in weight",IsBxGy=false,BuyX=0,GetY=0,IsDiscounted=true,DiscountPercentage=15, ProductId="111112"
                },
                 new Coupon
                {
                    PromoCode ="B3G1FR", PromoName="BTGO",PromoDescription="Buy 3 get 1 free in Bread",IsBxGy=true,BuyX=3,GetY=1,IsDiscounted=false,DiscountPercentage=0, ProductId="111113"
                },
                  new Coupon
                {
                    PromoCode ="B1G2FR", PromoName="BOGT",PromoDescription="Buy 1 Apple get 2 free ",IsBxGy=true,BuyX=1,GetY=2,IsDiscounted=false,DiscountPercentage=0, ProductId="111111"
                },
                 new Coupon
                {
                    PromoCode ="SPROMO", PromoName="SPecialPromo",PromoDescription="Buy 10 get 1 free and/or  10 percent off on Orange if buy in weight",IsBxGy=true,BuyX=10,GetY=1,IsDiscounted=true,DiscountPercentage=10, ProductId="111112"
                },
                new Coupon
                {
                    PromoCode ="XPROMO", PromoName="XclusivePromo",PromoDescription="15% discount on your purchase",ISFullPurchaseDiscount=true,FullPurchaseDiscount=15,ProductId="Any"
                    //IsBxGy=fasle,BuyX=3,GetY=1,IsDiscounted=true,DiscountPercentage=10
                }
            };

            return promos;
        }
    }
}
