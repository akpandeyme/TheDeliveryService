using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TheDeliveryService.Config;
using TheDeliveryService.Models;
using TheDeliveryService.Services;
using TheDeliveryService.Services.Interfaces;
using TheDeliveryService.Services.Strategies;

var services = new ServiceCollection();


var config = new DeliveryConfig();
services.AddSingleton(config);

var offers = new List<IDiscount>
{
    new Discount("OFR001", 10, 0, 200, 70, 200),
    new Discount("OFR002", 7, 5, 150, 100, 150),
    new Discount("OFR003", 5, 50, 250, 10, 150)
};
services.AddSingleton<IDiscountService>(new DiscountService(offers));

services.AddSingleton<IDeliveryStrategy, GWeightStrategy>();
services.AddSingleton<IDeliveryService, DeliveryService>();

var provider = services.BuildServiceProvider();

var offerService = provider.GetRequiredService<IDiscountService>();
var scheduler = provider.GetRequiredService<IDeliveryService>();

var firstLine = Console.ReadLine()?.Split(' ');
config.BaseCost = double.Parse(firstLine[0]);
int pkgCount = int.Parse(firstLine[1]);
var packages = new List<Package>();

for (int i = 0; i < pkgCount; i++)
{
    var parts = Console.ReadLine()?.Split(' ');
    packages.Add(new Package
    {
        Id = parts[0],
        Weight = double.Parse(parts[1]),
        Distance = double.Parse(parts[2]),
        OfferCode = parts[3]
    });
}

var vehicleInfo = Console.ReadLine()?.Split(' ');
config.NumVehicles = int.Parse(vehicleInfo[0]);
config.MaxSpeed = double.Parse(vehicleInfo[1]);
config.MaxWeight = double.Parse(vehicleInfo[2]);

offerService.ApplyDiscounts(packages, config.BaseCost);
scheduler.ScheduleDeliveries(packages, config.NumVehicles, config.MaxSpeed, config.MaxWeight);

foreach (var pkg in packages)
{
    Console.WriteLine($"{pkg.Id} {pkg.Discount:0} {pkg.TotalCost:0} {pkg.DeliveryTime:0.00}");
}
