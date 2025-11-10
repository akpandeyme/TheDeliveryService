using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeliveryService.Config
{
    public class DeliveryConfig
    {
        public double BaseCost { get; set; } = 100;
        public double MaxSpeed { get; set; } = 70;
        public double MaxWeight { get; set; } = 200;
        public int NumVehicles { get; set; } = 2;
    }
}
