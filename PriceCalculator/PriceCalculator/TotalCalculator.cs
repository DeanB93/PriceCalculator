using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    public class TotalCalculator
    {

        public double GetSubtotal(ShoppingCart shoppingCart)
        {
            return shoppingCart.CartContents.Sum(c => c.Price);
        }

        public double GetTotalAfterDiscount(ShoppingCart shoppingCart, List<Discount> currentPromotions)
        {
            //Identify items that trigger a discount
            var promotionalItemsInCart = currentPromotions.Select(p => p.PromotionalItem)
                .Intersect(shoppingCart.CartContents.Select(c => c.Name));
            //running total of discount
            var totalDiscount = 0.00;
            //check if any promotional items are in cart
            if (promotionalItemsInCart.Any())
            {
                foreach (var promoItem in promotionalItemsInCart)
                {
                    //shorlist of viable discounts
                    var applicablePromos = currentPromotions.Where(c => c.PromotionalItem == promoItem);
                    //In case multiple deals are a thing in the future!
                    foreach (var promo in applicablePromos)
                    {
                        //If discounted item is the same as the promotional item, we need to modify the 'discount batch' counter.
                        var sameItemModifier = 0;
                        if (promo.PromotionalItem == promo.DiscountedItem)
                        {
                            sameItemModifier = 1;
                        }

                        //check if discount is triggered
                        if (promo.QuantityRequired <= shoppingCart.CartContents.Where(c => c.Name == promo.PromotionalItem).Count())
                            {
                                
                                //figure out how many 'deals' are stacked
                                var discountedItem = shoppingCart.CartContents.Where(c => c.Name == promo.DiscountedItem).FirstOrDefault();
                                int numberOfDeals = shoppingCart.CartContents.Where(c => c.Name == promo.PromotionalItem).Count() / (promo.QuantityRequired + 0);

                                //apply the discount
                                totalDiscount = totalDiscount + (numberOfDeals * (promo.DiscountPercent * discountedItem.Price));
                            }
                    }
                }

            }

            return Math.Round((double)GetSubtotal(shoppingCart) - totalDiscount, 2) ;

        }
    }
}
