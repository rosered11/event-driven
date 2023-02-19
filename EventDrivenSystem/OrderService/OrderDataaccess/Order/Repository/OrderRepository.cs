using Microsoft.EntityFrameworkCore;
using Rosered11.OrderService.Common.DataAccess;
using Rosered11.OrderService.DataAccess.Entity;

namespace Rosered11.OrderService.DataAccess.Repository
{
    public class OrderRepository : Repository<OrderEntity>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}