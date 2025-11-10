using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;
using TheDeliveryService.Services.Interfaces;
using TheDeliveryService.Utils;

namespace TheDeliveryService.Services
{
    public class DeliveryService:IDeliveryService
    {
        private readonly IDeliveryStrategy _strategy;

        public DeliveryService(IDeliveryStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void ScheduleDeliveries(List<Package> packages, int numVehicles, double maxSpeed, double maxWeight)
        {
            var vehicles = Vehicle.CreateFleet(numVehicles); 
            var vehicleQueue = new VehicleQueue(vehicles);

            if (packages == null || !packages.Any())
                throw new ArgumentException("Package list cannot be empty.", nameof(packages));

            if (maxWeight <= 0 || maxSpeed <= 0)
                throw new ArgumentException("Max weight and speed must be greater than zero.");

            var groupedShipments = _strategy.GroupPackages(packages, maxWeight);

            foreach (var batch in groupedShipments)
            {
                var vehicle = vehicleQueue.GetNextAvailableVehicle();
                double farthestDistance = batch.Max(p => p.Distance);
                double oneWayTime = farthestDistance / maxSpeed;
                double roundTripTime = 2 * oneWayTime;

                foreach (var pkg in batch)
                    pkg.DeliveryTime = vehicle.AvailableAt + oneWayTime;

                vehicleQueue.UpdateVehicleAvailability(vehicle, roundTripTime);
            }
        }
    }
}
