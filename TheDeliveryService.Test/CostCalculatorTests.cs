using System;
using TheDeliveryService.Models;
using TheDeliveryService.Services;

namespace TheDeliveryService.Test
{
    [TestFixture]
    public class CostCalculatorTests
    {
        [Test]
        public void Should_Calculate_Correct_TotalCost_And_Discount()
        {
            double baseCost = 100;

            var offers = new List<IDiscount>
            {
                new Discount("OFR001", 10, 0, 200, 70, 200),
                new Discount("OFR002", 7, 5, 150, 100, 150),
                new Discount("OFR003", 5, 50, 250, 10, 150)
            };

            var CostCalculator = new DiscountService(offers);
            var pkg = new Package { Id = "PKG4", Weight = 110, Distance = 60, OfferCode = "OFR002" };
            CostCalculator.ApplyDiscounts(new List<Package> { pkg }, baseCost);

            double expectedCost = 100 + (110 * 10) + (60 * 5); // 100 + 1100 + 300 = 1500
            double expectedDiscount = expectedCost * 0.07;     // 105
            double expectedTotal = expectedCost - expectedDiscount;

            Assert.AreEqual(expectedDiscount, pkg.Discount, 0.01);
            Assert.AreEqual(expectedTotal, pkg.TotalCost, 0.01);
        }

        [Test]
        public void Should_Not_Apply_Discount_When_Offer_Not_Applicable()
        {
            double baseCost = 100;

            var offers = new List<IDiscount>
            {
                new Discount("OFR001", 10, 0, 200, 70, 200), 
            };

            var CostCalculator = new DiscountService(offers);
            var pkg = new Package { Id = "PKG1", Weight = 50, Distance = 30, OfferCode = "OFR001" };
            CostCalculator.ApplyDiscounts(new List<Package> { pkg }, baseCost);

            double expectedCost = 100 + (50 * 10) + (30 * 5); // 100 + 500 + 150 = 750
            Assert.AreEqual(0, pkg.Discount);
            Assert.AreEqual(expectedCost, pkg.TotalCost);
        }
    }
}