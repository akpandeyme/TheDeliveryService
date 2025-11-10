README
-------

DELIVERY COST AND TIME ESTIMATION SYSTEM

A C# console application that calculates delivery cost, applicable discounts, and estimated delivery time for multiple packages transported by limited vehicles.

PROBLEM OVERVIEW

Kiki runs a courier company and needs a system to:

Calculate total delivery cost per package including discounts.

Schedule multiple deliveries efficiently using limited vehicles.

Estimate delivery time per package based on distance, weight, and vehicle speed.

SAMPLE INPUT

Example Input:

100 5
PKG1 50 30 OFR001
PKG2 75 125 OFFR0008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
2 70 200

Meaning:

Base delivery cost: 100

Number of packages: 5

Package format: <ID> <Weight> <Distance> <OfferCode>

Vehicle information: <NoOfVehicles> <MaxSpeed> <MaxWeight>

APPROACH AND CALCULATIONS

Step 1: COST CALCULATION

Total Cost = BaseCost + (Weight * 10) + (Distance * 5)

If an offer is applicable:
Discount = TotalCost * (OfferPercentage / 100)
Final Cost = TotalCost - Discount

Step 2: DELIVERY TIME CALCULATION

Packages are grouped for delivery based on vehicle capacity.

Each trip’s time depends on the farthest package distance.

Round Trip Time = 2 * (FarthestDistance / VehicleSpeed)
Delivery Time (per package) = VehicleAvailableAt + (FarthestDistance / VehicleSpeed)

After each trip, the vehicle becomes available again after completing its return trip.

VEHICLE SCHEDULING LOGIC

Vehicles are selected based on earliest availability.
Packages are batched without exceeding maximum weight.
A priority queue (min-heap) is used to always assign the next available vehicle.
Each delivery updates the vehicle’s availability time.

PROJECT STRUCTURE

KikiCourier/
Models/
Package.cs
Vehicle.cs
Offers/
IOffer.cs
Offer.cs
Services/
CostCalculator.cs
DeliveryScheduler.cs
Utils/
VehicleQueue.cs
Program.cs

Component Responsibilities:

Package: Represents package information and computed data.
Vehicle: Tracks vehicle ID and availability time.
Offer: Represents an offer and its applicability conditions.
CostCalculator: Computes total cost and applicable discounts.
DeliveryScheduler: Manages grouping, trip times, and vehicle reuse.
VehicleQueue: Handles vehicles in a priority queue based on availability.

EXAMPLE OUTPUT

PKG1 0 750 1.79
PKG2 0 1475 1.79
PKG3 0 2350 1.43
PKG4 105 1395 3.57
PKG5 0 2125 2.86

Columns:
<PackageID> <Discount> <TotalCostAfterDiscount> <EstimatedDeliveryTime>

TESTING (NUNIT)

A separate test project "KikiCourier.Tests" validates the logic.
Test Files:
OfferTests.cs: Tests offer applicability.
CostCalculatorTests.cs: Validates total cost and discount logic.
VehicleTests.cs: Confirms vehicle initialization and reuse.
DeliverySchedulerTests.cs: Validates batch scheduling and delivery time estimation.

Run tests using:
dotnet test

All tests should pass if the system logic is correct.

HOW TO RUN THE APPLICATION

Build:
dotnet build

Run:
dotnet run --project TheDeliveryService

Provide input (as per format shown above).

Observe calculated output in the console.

DESIGN FEATURES

Extensible: Easily modify offers, pricing, or scheduling rules.
Maintainable: Clear class separation and single-responsibility design.
Efficient: Uses greedy batching and priority queue-based vehicle scheduling.
Testable: Fully automated NUnit tests ensure correctness.

FUTURE ENHANCEMENTS

Add GUI input/output interface.
Read offer configurations from JSON or database.
Include detailed trip summaries and statistics.
Add exception handling and logging mechanisms.
Extend with dynamic route optimization.

AUTHOR

The Delivery Estimation System
Developed in C# (.NET 8)
Designed for performance, maintainability, and scalability.
Includes automated NUnit test coverage.