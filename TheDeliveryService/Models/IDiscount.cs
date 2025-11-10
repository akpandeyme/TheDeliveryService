using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeliveryService.Models
{
    public interface IDiscount
    {
        string DiscountCode { get; }
        bool IsValid(double distance, double weight);
        double DiscountPercentage { get; }
    }
}
