using TheDeliveryService.Models;

namespace TheDeliveryService.Services.Interfaces
{
    public interface IDeliveryStrategy
    {
        List<List<Package>> GroupPackages(List<Package> packages, double maxWeight);
    }
}
