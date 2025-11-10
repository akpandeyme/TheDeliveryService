using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;

namespace TheDeliveryService.Services.Interfaces
{
    public interface IDiscountService
    {
        void ApplyDiscounts(List<Package> packages, double baseCost);
    }
}
