using Rosered11.OrderService.Domain.Entities;

namespace Rosered11.OrderService.Domain.Ports.Output.Repository
{
    public interface RestaurantRepository
    {
        Restaurant FindRestaurantInformation(Restaurant restaurant);
    }
}