using System;
using TheDeliveryService.Models;
using TheDeliveryService.Services;
using TheDeliveryService.Services.Strategies;

namespace TheDeliveryService.Test
{
    [TestFixture]
    public class DeliverySchedulerTests
    {
        [Test]
        public void Should_Assign_DeliveryTimes_Correctly()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 50, Distance = 30 },
                new Package { Id = "PKG2", Weight = 75, Distance = 125 },
                new Package { Id = "PKG3", Weight = 175, Distance = 100 },
                new Package { Id = "PKG4", Weight = 110, Distance = 60 },
                new Package { Id = "PKG5", Weight = 155, Distance = 95 }
            };

            var vehicles = Vehicle.CreateFleet(2);
            double maxWeight = 200;
            double maxSpeed = 70;
            int vehCount = vehicles.Count;

            var strategy = new GWeightStrategy();
            var scheduler = new DeliveryService(strategy);

            scheduler.ScheduleDeliveries(packages, vehCount, maxSpeed, maxWeight);

            var deliveryTimes = packages.OrderBy(p => p.DeliveryTime).Select(p => p.DeliveryTime).ToList();

            Assert.That(deliveryTimes, Is.Ordered.Ascending);
            Assert.IsTrue(deliveryTimes.All(t => t > 0));
        }

        [Test]
        public void Should_Not_Assign_Weight_Over_Vehicle_Capacity()
        {
            var packages = new List<Package>
            {
                new Package { Id = "PKG1", Weight = 180, Distance = 50 },
                new Package { Id = "PKG2", Weight = 50, Distance = 60 }
            };

            var vehicles = Vehicle.CreateFleet(1);
            double maxWeight = 200;
            double maxSpeed = 70;
            int vehCount = vehicles.Count;

            var strategy = new GWeightStrategy();
            var scheduler = new DeliveryService(strategy);

            scheduler.ScheduleDeliveries(packages, vehCount, maxSpeed, maxWeight);

            Assert.IsTrue(packages.All(p => p.Weight <= maxWeight));
        }
    }
}