using CashRegister.Domain.Abstract;
using CashRegister.Domain.Concrete;
using CashRegister.ExecuteBusinessLogic.Logic.Abstract;
using CashRegister.ExecuteBusinessLogic.Logic.ProductLogic;
using CashRegister.ExecuteBusinessLogic.Model;
using CashRegister.Model.UIHelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.ExecuteUiBusinessLogic
{
    public class CartEditor
    {
        private readonly IProductOperation _pOp;
        private readonly string _wKey;
        private readonly string _qKey;
        public Dictionary<string, CartInfo> inventory;

        public CartEditor(IProductOperation pOp)
        {
            _pOp = pOp;
            inventory = new Dictionary<string, CartInfo>();
            //Cart helper keys represent whether an item is sold by quantity or weight
            _wKey = "w";
            _qKey = "q";
        }

        public Dictionary<string, CartInfo> Cart()
        {
            var exit = "10";
            while (!exit.Equals("e"))
            {
                Console.Write("Scann the item :");
                var scanerCode = Console.ReadLine();
                try
                {
                    var product = _pOp.FindProductAsync(scanerCode).Result;
                    if (product != null)
                    {
                        if (product.IsSoldByQuantity && product.IsSoldByWeight) DoDoubleOptionOperation(product);
                        else if (product.IsSoldByQuantity) DoOperationForQuantityOption(product);
                        else DoOperationForWeightOption(product);

                    }
                    else
                    {
                        Console.WriteLine("This item is not exist in Record, plesae scan again or enter miscellaneous item code");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("An error occured, please start over");
                    break;
                }
                Console.WriteLine();
                Console.Write("Wanna add more item? if yes press any key, Press 'e' or 'E' to for receipt :");
                exit = Console.ReadLine();
                Console.WriteLine();

            }

            return inventory;
        }


        #region UIExtensionMethods
        // Methods used to add the item to cart properly based on item scaned by weight or by quantity
        private void DoDoubleOptionOperation(Product product)
        {
            Console.Write("How does customer want to buy?If Weight enter {0} and if by Quantity enter {1} :", _qKey, _wKey);
            bool buyOptionValid = false;

            while (!buyOptionValid)
            {
                var buyingOption = Console.ReadLine();
                if (!string.IsNullOrEmpty(buyingOption) && !string.IsNullOrWhiteSpace(buyingOption) && (buyingOption.Equals(_wKey) || buyingOption.Equals(_wKey.ToLower()) || buyingOption.Equals(_qKey) || buyingOption.Equals(_qKey.ToLower())))
                {
                    buyOptionValid = true;

                    if (buyingOption.Equals("w") || buyingOption.Equals("W"))
                    {
                        DoOperationForWeightOption(product);
                    }
                    else
                    {
                        DoOperationForQuantityOption(product);
                    }
                }
                else
                {
                    Console.Write("Invalid entry! \nPlease enter correct Character?If Weight enter 'w' and if by Quantity enter 'q' :");
                }
            }
        }

        // Method used to add the item to cart properly based on item scaned by quantity
        private void DoOperationForQuantityOption(Product product)
        {
            if (product.ProductName == "Miscellaneous")
            {
                product.PricePerUnit = SetPriceForMisclleneousItem();
            }
            Console.Write("Enter the counted number of last scanned item: ");
            bool valid = false;

            while (!valid)
            {
                var countEntered = Console.ReadLine();

                if (int.TryParse(countEntered, out int number))
                {
                    int count = Convert.ToInt32(countEntered);
                    valid = true;
                    var productInfo = new CartInfo
                    {
                        Product = product,
                        Count = new Count
                        {
                            Total = count
                        },
                        Weight = new Weight()

                    };
                    var helperId = string.Concat(product.Sku, "-", _qKey);

                    AddToCart(helperId, productInfo);
                }
                else
                {
                    Console.WriteLine("Please enter a valid number such as 123");
                }
            }
        }

        // Methods used to add the item to cart properly based on item scaned by weight
        private void DoOperationForWeightOption(Product product)
        {
            if (product.ProductName == "Miscellaneous")
            {
                product.PricePerPound = SetPriceForMisclleneousItem();
            }
            Console.WriteLine();

            Console.Write("Enter the measured weight in lb: ");
            bool valid = false;
            while (!valid)
            {
                var weightEntered = Console.ReadLine();

                if (double.TryParse(weightEntered, out double number))
                {
                    double weight = Convert.ToDouble(weightEntered);
                    valid = true;
                    var productInfo = new CartInfo
                    {
                        Product = product,
                        Count = new Count(),
                        Weight = new Weight
                        {
                            WeightTotal = weight,
                            DiscountedPrice = product.PricePerPound
                        }
                    };
                    var helperId = string.Concat(product.Sku, "-", _wKey);

                    AddToCart(helperId, productInfo);

                }
                else
                {
                    Console.WriteLine("Please enter a valid number such as 0.01");
                }
            }


        }

        // Set the price for Misclleneous items
        private double SetPriceForMisclleneousItem()
        {
            Console.Write("Enter price for scanned Miscellaneous item: ");
            var valid = false;

            while (!valid)
            {
                var priceEntered = Console.ReadLine();

                if (double.TryParse(priceEntered, out double number))
                {
                    double price = Convert.ToDouble(priceEntered);
                    valid = true;

                    return price;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number such as 0.01");
                }
            }
            return 0;
        }

        // Used to add a valid item to card
        private void AddToCart(string helperId, CartInfo itemAdded)
        {

            if (inventory.ContainsKey(helperId))
            {
                if (helperId.EndsWith(_qKey))
                {
                    inventory[helperId].Count.Total = itemAdded.Count.Total;
                }
                else
                {
                    inventory[helperId].Weight.WeightTotal += itemAdded.Weight.WeightTotal;
                }
            }
            else
            {
                inventory.Add(helperId, itemAdded);

            }
        }

        #endregion
    }
}
