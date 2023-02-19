using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.DataAccess.Exception;
using Rosered11.OrderService.Domain.Entities;

namespace Rosered11.OrderService.DataAccess.Mapper
{
    public class RestaurantDataAccessMapper
    {
        public List<Guid> RestaurantToRestaurantProducts(Restaurant restaurant)
        {
            return restaurant.Products?.Select(x => x.Id.GetValue()).ToList() ?? new List<Guid>();
        }

        public Restaurant RestaurantEntityToRestaurant(List<RestaurantEntity> restaurantEntities)
        {
            if (!restaurantEntities.Any())
                throw new RestaurantDataAccessException("Restaurant could not be found!");
            RestaurantEntity restaurantEntity = restaurantEntities.First();

            List<Product> restaurantProduct = restaurantEntities.Select(x => new Product( new ProductId(x.ProductId), x.ProductName ?? string.Empty, new Money(x.ProductPrice))).ToList();

            return Restaurant.NewBuilder()
                .SetRestaurantId(new RestaurantId(restaurantEntity.Id))
                .SetProducts(restaurantProduct)
                .SetActive(restaurantEntity.RestaurantActive)
                .Build();
        }
    }
}