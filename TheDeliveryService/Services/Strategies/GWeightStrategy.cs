using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;
using TheDeliveryService.Services.Interfaces;

namespace TheDeliveryService.Services.Strategies
{
    public class GWeightStrategy : IDeliveryStrategy
    {
        public List<List<Package>> GroupPackages(List<Package> packages, double maxWeight)
        {
            var pending = packages.OrderByDescending(p => p.Weight).ToList();
            var result = new List<List<Package>>();

            while (pending.Any())
            {
                double total = 0;
                var batch = new List<Package>();

                foreach (var pkg in pending.ToList())
                {
                    if (total + pkg.Weight <= maxWeight)
                    {
                        batch.Add(pkg);
                        total += pkg.Weight;
                        pending.Remove(pkg);
                    }
                }
                result.Add(batch);
            }

            return result;
        }
    }
}
