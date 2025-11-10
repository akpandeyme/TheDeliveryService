using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeliveryService.Models
{
    public class Discount : IDiscount
    {
        public string DiscountCode { get; private set; }
        public double DiscountPercentage { get; private set; }
        private double MinDistance, MaxDistance, MinWeight, MaxWeight;

        public Discount(string code, double discount, double minDistance, double maxDistance, double minWeight, double maxWeight)
        {
            DiscountCode = code;
            DiscountPercentage = discount;
            MinDistance = minDistance;
            MaxDistance = maxDistance;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
        }

        public bool IsValid(double distance, double weight)
        {
            return distance >= MinDistance && distance <= MaxDistance &&
                   weight >= MinWeight && weight <= MaxWeight;
        }
    }

}

