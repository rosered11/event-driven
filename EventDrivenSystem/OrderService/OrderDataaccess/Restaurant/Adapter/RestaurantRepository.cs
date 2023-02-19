using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.DataAccess.Mapper;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Ports.Output.Repository;

namespace Rosered11.OrderService.DataAccess.Adapter
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly Repository.RestaurantRepository _restaurantRepository;
        private readonly RestaurantDataAccessMapper _restaurantDataAccessMapper;

        public RestaurantRepository(Repository.RestaurantRepository restaurantRepository, RestaurantDataAccessMapper restaurantDataAccessMapper)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantDataAccessMapper = restaurantDataAccessMapper;
        }
        public Restaurant FindRestaurantInformation(Restaurant restaurant)
        {
            List<Guid> restaurantProducts = _restaurantDataAccessMapper.RestaurantToRestaurantProducts(restaurant);

            List<RestaurantEntity> restaurantEntities = _restaurantRepository.FindByRestaurantIdAndProductId(restaurant.Id.GetValue(), restaurantProducts);

            return _restaurantDataAccessMapper.RestaurantEntityToRestaurant(restaurantEntities);
        }
    }
}