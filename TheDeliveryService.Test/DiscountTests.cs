using System;
using TheDeliveryService.Models;

namespace TheDeliveryService.Test
{
    [TestFixture]
    public class DiscountTests
    {
        [Test]
        public void Offers_ShouldApplyCorrectly()
        {
            var offers = new[]
            {
                new Discount("OFR001", 10, 0, 200, 70, 200),
                new Discount("OFR002", 7, 5, 150, 100, 150),
                new Discount("OFR003", 5, 50, 250, 10, 150)
            };

            var pkg1 = new Package { Weight = 50, Distance = 30, OfferCode = "OFR001" };
            var pkg2 = new Package { Weight = 110, Distance = 60, OfferCode = "OFR002" };
            var pkg3 = new Package { Weight = 150, Distance = 100, OfferCode = "OFR003" };

            var offer1 = offers.First(o => o.DiscountCode == pkg1.OfferCode);
            var offer2 = offers.First(o => o.DiscountCode == pkg2.OfferCode);
            var offer3 = offers.First(o => o.DiscountCode == pkg3.OfferCode);

            Assert.IsFalse(offer1.IsValid(pkg1.Distance, pkg1.Weight), "OFR001 not applicable for pkg1");
            Assert.IsTrue(offer2.IsValid(pkg2.Distance, pkg2.Weight), "OFR002 applicable for pkg2");
            Assert.IsTrue(offer3.IsValid(pkg3.Distance, pkg3.Weight), "OFR003 applicable for pkg3");
        }
    }
}