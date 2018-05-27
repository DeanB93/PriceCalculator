using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator
{
    public class Discount
    {
        public string Description { get; set; }
        public string PromotionalItem { get; set; }
        public string DiscountedItem { get; set; }
        public int QuantityRequired { get; set; }
        public double DiscountPercent { get; set; }
    }
}
