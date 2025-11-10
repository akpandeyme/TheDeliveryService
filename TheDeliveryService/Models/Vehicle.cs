using System;
using System.Collections.Generic;

namespace TheDeliveryService.Models
{
    public class Vehicle 
    {
        public int Id { get; set; }
        public double AvailableAt { get; set; } = 0;

        public static List<Vehicle> CreateFleet(int count)
        {
            var fleet = new List<Vehicle>(count);
            for (int i = 1; i <= count; i++)
                fleet.Add(new Vehicle { Id = i });
            return fleet;
        }
    }
}
