using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceCalculator;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculatorTests
{
    [TestClass]
    public class UnitTest1
    {
        ShoppingCart productDatabase = new ShoppingCart();
        List<Discount> discountDatabase = new List<Discount>();
        TotalCalculator totalCalculator = new TotalCalculator();

        [TestInitialize()]
        public void Initialize()
        {
            //Using a shopping cart as a 'database' of items
            productDatabase.CartContents.Add(new Product { Name = "Butter", Price = .8});
            productDatabase.CartContents.Add(new Product { Name = "Milk", Price = 1.15});
            productDatabase.CartContents.Add(new Product { Name = "Bread", Price = 1});

            //using a list of discounts to hold all possible deals
            discountDatabase.Add(new Discount { Description = "2 Butter, Bread 50%", DiscountPercent = .5, PromotionalItem = "Butter", QuantityRequired = 2, DiscountedItem = "Bread" });
            discountDatabase.Add(new Discount { Description = "3 Milk, Milk 100%", DiscountPercent = 1, PromotionalItem = "Milk", QuantityRequired = 3, DiscountedItem = "Milk" });
        }

        [TestCleanup()]
        public void Cleanup()
        {
            productDatabase.CartContents.Clear();
            discountDatabase.Clear();
        }

        [TestMethod]
        public void GivenOneOfEachItem_NoDiscountShouldBeApplied()
        {
            var testCart = new ShoppingCart();
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Butter"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Bread"));

            //verify total and subtotal are the same
            var subtotal = totalCalculator.GetSubtotal(testCart);
            var total = totalCalculator.GetTotalAfterDiscount(testCart, discountDatabase);

            Assert.AreEqual(total, subtotal);

            //verify total
            Assert.AreEqual(2.95, total);
        }

        [TestMethod]
        public void GivenTwoButterAndBread_DiscountShouldBeApplied()
        {
            var testCart = new ShoppingCart();
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Butter"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Butter"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Bread"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Bread"));

            var subtotal = totalCalculator.GetSubtotal(testCart);
            var total = totalCalculator.GetTotalAfterDiscount(testCart, discountDatabase);

            //verify total and subtotal are not the same
            Assert.AreNotEqual(subtotal, total);

            Assert.AreEqual(3.10, total);
        }


        [TestMethod]
        public void GivenFourMilk_DiscountShouldBeApplied()
        {
            var testCart = new ShoppingCart();
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));

            var subtotal = totalCalculator.GetSubtotal(testCart);
            var total = totalCalculator.GetTotalAfterDiscount(testCart, discountDatabase);

            //verify total and subtotal are not the same
            Assert.AreNotEqual(subtotal, total);

            Assert.AreEqual(3.45, total);
        }

        [TestMethod]
        public void GivenTwoButterOneBreadEightMilk_DiscountShouldBeApplied()
        {
            var testCart = new ShoppingCart();
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Milk"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Butter"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Butter"));
            testCart.CartContents.Add(productDatabase.CartContents.FirstOrDefault(c => c.Name == "Bread"));

            var subtotal = totalCalculator.GetSubtotal(testCart);
            var total = totalCalculator.GetTotalAfterDiscount(testCart, discountDatabase);

            //verify total and subtotal are not the same

            Assert.AreNotEqual(subtotal, total);

            Assert.AreEqual(9.00, total);
        }
    }
}
