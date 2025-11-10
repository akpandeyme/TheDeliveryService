using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeliveryService.Models;

namespace TheDeliveryService.Utils
{
    public class VehicleQueue
    {
        private readonly PriorityQueue<Vehicle, double> _queue;

        public VehicleQueue(IEnumerable<Vehicle> vehicles)
        {
            _queue = new PriorityQueue<Vehicle, double>();
            foreach (var v in vehicles)
                _queue.Enqueue(v, v.AvailableAt);
        }

        public Vehicle GetNextAvailableVehicle()
        {
            _queue.TryDequeue(out var vehicle, out _);
            return vehicle!;
        }

        public void UpdateVehicleAvailability(Vehicle vehicle, double roundTripTime)
        {
            vehicle.AvailableAt += roundTripTime;
            _queue.Enqueue(vehicle, vehicle.AvailableAt);
        }
    }
}
