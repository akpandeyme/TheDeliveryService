using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;
using TheDeliveryService.Services.Interfaces;

namespace TheDeliveryService.Services.Strategies
{
    public class DWeightStrategy : IDeliveryStrategy
    {
        public List<List<Package>> GroupPackages(List<Package> packages, double maxWeight)
        {
            var sorted = packages.OrderByDescending(p => p.Distance / p.Weight).ToList();
            var result = new List<List<Package>>();

            while (sorted.Any())
            {
                double currentWeight = 0;
                var batch = new List<Package>();

                foreach (var pkg in sorted.ToList())
                {
                    if (currentWeight + pkg.Weight <= maxWeight)
                    {
                        batch.Add(pkg);
                        currentWeight += pkg.Weight;
                        sorted.Remove(pkg);
                    }
                }

                result.Add(batch);
            }

            return result;
        }
    }
}
