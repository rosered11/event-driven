using Rosered11.OrderService.Common.DataAccess;

namespace Rosered11.OrderService.DataAccess.Entity
{
    public class CustomerEntity : BaseEntity<Guid>
    {
        public CustomerEntity(Guid id) : base(id)
        {
        }
    }
}