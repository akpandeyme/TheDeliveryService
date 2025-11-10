using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;
using TheDeliveryService.Services.Interfaces;

namespace TheDeliveryService.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly List<IDiscount> _discounts;

        public DiscountService(IEnumerable<IDiscount> discounts)
        {
            _discounts = discounts.ToList();
        }

        public void ApplyDiscounts(List<Package> packages, double baseCost)
        {
            foreach (var pkg in packages)
            {
                double totalCost = baseCost + (pkg.Weight * 10) + (pkg.Distance * 5);
                var offer = _discounts.FirstOrDefault(o => o.DiscountCode.Equals(pkg.OfferCode, StringComparison.OrdinalIgnoreCase));

                double discount = (offer != null && offer.IsValid(pkg.Distance, pkg.Weight))
                    ? totalCost * (offer.DiscountPercentage / 100)
                    : 0;

                pkg.Discount = discount;
                pkg.TotalCost = totalCost - discount;
            }
        }
    }
}

