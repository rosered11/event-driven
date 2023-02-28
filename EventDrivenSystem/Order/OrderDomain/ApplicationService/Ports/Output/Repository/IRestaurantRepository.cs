using Rosered11.Order.Domain.Core.Entity;

namespace Rosered11.Order.Application.Service.Ports.Output.Repository;

public interface IRestaurantRepository {

    Restaurant findRestaurantInformation(Restaurant restaurant);
}