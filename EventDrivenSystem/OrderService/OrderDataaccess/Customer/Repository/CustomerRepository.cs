using Microsoft.EntityFrameworkCore;
using Rosered11.OrderService.Common.DataAccess;
using Rosered11.OrderService.DataAccess.Entity;

namespace Rosered11.OrderService.DataAccess.Repository
{
    public class CustomerRepository : Repository<CustomerEntity>
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }
    }
}