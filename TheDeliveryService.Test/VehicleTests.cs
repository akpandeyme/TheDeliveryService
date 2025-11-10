using System;
using TheDeliveryService.Models;
using TheDeliveryService.Services;

namespace TheDeliveryService.Test
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void Should_Create_Vehicle_Fleet_With_Sequential_Ids()
        {
            var vehicles = Vehicle.CreateFleet(3);

            Assert.AreEqual(3, vehicles.Count);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, vehicles.Select(v => v.Id).ToArray());
            Assert.IsTrue(vehicles.All(v => v.AvailableAt == 0));
        }

        [Test]
        public void Should_Update_Availability_After_Trip()
        {
            var vehicle = new Vehicle { Id = 1, AvailableAt = 0 };
            double tripTime = 2.5;

            vehicle.AvailableAt += tripTime;
            Assert.AreEqual(2.5, vehicle.AvailableAt);
        }
    }
}