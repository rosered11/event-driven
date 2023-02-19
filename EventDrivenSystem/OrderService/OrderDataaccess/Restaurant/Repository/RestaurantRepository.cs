using Microsoft.EntityFrameworkCore;
using Rosered11.OrderService.Common.DataAccess;
using Rosered11.OrderService.DataAccess.Entity;

namespace Rosered11.OrderService.DataAccess.Repository
{
    public class RestaurantRepository : Repository<RestaurantEntity>
    {
        public RestaurantRepository(DbContext context) : base(context)
        {
        }

        public List<RestaurantEntity> FindByRestaurantIdAndProductId(Guid restaurantId, List<Guid> productIds)
        {
            // _context.Find(restaurantId, null);
            return new List<RestaurantEntity>();
        }
    }
}