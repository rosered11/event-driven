using Rosered11.Common.DataAcess.Entity;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Domain.Core.Entity;

namespace Rosered11.Order.DataAccess.Restaurant;

public class RestaurantDataAccessMapper
{
    public IEnumerable<Guid> restaurantToRestaurantProducts(Domain.Core.Entity.Restaurant restaurant) {
        return restaurant.Products.Select(product => product.ID.GetValue()).ToList();
    }

    public Domain.Core.Entity.Restaurant? restaurantEntityToRestaurant(IEnumerable<RestaurantEntity> restaurantEntities) {

        RestaurantEntity restaurantEntity = restaurantEntities.FirstOrDefault();
        if (restaurantEntity != null)
        {
            List<Product> restaurantProducts = restaurantEntities.Select(entity => 
            new Product(new(entity.ProductId), entity.ProductName, new Money(entity.ProductPrice))
            ).ToList();
        
            return new Domain.Core.Entity.Restaurant(new(restaurantEntity.RestaurantId)){
                Products = restaurantProducts,
                Active = restaurantEntity.RestaurantActive
            };
        }
        return null;
    }
}