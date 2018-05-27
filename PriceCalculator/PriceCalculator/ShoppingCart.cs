using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    public class ShoppingCart
    {
        public List<Product> CartContents { get; set; }
        public ShoppingCart()
        {
            CartContents = new List<Product>();
        }

    }
}
